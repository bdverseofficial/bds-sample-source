<template>
  <v-container pa-6 ma-6>
    <v-row>
      <v-col cols="3">
        <template v-if="result && result.groups">
          <v-list-item>
            <v-list-item-title>{{$t("SEARCH.GROUPS")}}</v-list-item-title>
          </v-list-item>

          <v-list dense shaped>
            <v-list-item
              v-for="(item, index) in result.groups.items"
              :key="index"
              @click="search(item.uri)"
              v-model="item.selected"
            >
              <v-list-item-title>
                <span v-for="n in item.level - 1" :key="n" style="margin-left:15px;">&nbsp;</span>
                {{item.group.localName || item.group.name}}
              </v-list-item-title>
            </v-list-item>
          </v-list>
        </template>

        <template v-if="result && result.facets && result.items">
          <v-list-item>
            <v-list-item-title>{{$t("SEARCH.FILTERS")}}</v-list-item-title>
          </v-list-item>
          <v-list dense shaped v-for="(item, indexFacet) in result.facets.items" :key="indexFacet">
            <v-list-group :value="item.selected">
              <template v-slot:activator>
                <v-list-item-title v-text="item.facet.localName || item.facet.name"></v-list-item-title>
                <v-list-item-icon v-if="item.selected" @click="search(item.uri)">
                  <v-icon>check</v-icon>
                </v-list-item-icon>
              </template>
              <v-list-item
                v-for="(value, indexValue) in item.values"
                :key="indexValue"
                @click="search(value.uri)"
                v-model="value.selected"
              >
                <v-list-item-title v-text="value.facetValue.localName || value.facetValue.name"></v-list-item-title>
                <v-list-item-action>{{value.count}}</v-list-item-action>
              </v-list-item>
            </v-list-group>
          </v-list>
        </template>
      </v-col>
      <v-col cols="9">                    
        <v-container fluid>
            <v-row>
                <v-col cols="12">
                <v-text-field
              label="Search"
              placeholder="Search here for anything..."
              prepend-icon="search"
              v-model.lazy="request.searchKey"
              @change="search"
            ></v-text-field>
            </v-col>
            </v-row>
          <v-row>
            <v-col cols="6"></v-col>
            <v-col cols="6">
              <v-select
                :label="$t('SEARCH.ORDER')"
                :items="sortChoices"
                v-model="sortChoice"
                item-text="text"
                item-value="value"
              ></v-select>
            </v-col>
          </v-row>
        </v-container>        
        <v-list three-line v-if="result" class="data-table-view">
          <template v-for="(item, index) in result.items">
            <v-list-item :key="'I' + index">
              <v-list-item-content>
                <v-list-item-title>
                  <b>{{item.item.localName || item.item.name}}</b>
                </v-list-item-title>
                <v-list-item-subtitle>
                    <span>{{$t("SEARCH.GROUPSIZE")}}:{{item.item.groupSize}} </span>
                    <span>{{$t("SEARCH.GROUPTYPE")}}:{{item.item.groupType}} </span>
                </v-list-item-subtitle>
              </v-list-item-content>
              <v-list-item-action v-if="profileStore.me">
                <template v-if="profileStore.me.preferredSport && profileStore.me.preferredSport.key == item.item.key">
                  <v-btn icon><v-icon>star</v-icon></v-btn>
                </template>
                <template v-else>
                 <v-btn icon @click="updateSport(item.item)"><v-icon>star_border</v-icon></v-btn>
                </template>
              </v-list-item-action>
            </v-list-item>
            <v-divider v-if="index + 1 < result.items.length" :key="index"></v-divider>
          </template>
        </v-list>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts">
import Vue from "vue";
import Component from "vue-class-component";
import { Prop, Watch } from "vue-property-decorator";
import { Sport } from "@/models/bds";
import { ApiRequestConfig, SearchEntityResponse, SearchRequest, SearchEntityRequest } from "@bdverse/bds-sdk-vue";
import { ProfileStore } from '@bdverse/bds-sdk-vue/types/services/profileService';

@Component({
  name: "Search",
  filters: {
    toSortChoice: (result: SearchEntityResponse | null): string | undefined => {
      if (result && result.parameters && result.parameters.sortField) {
        return result.parameters.sortField + "|" + result.parameters.sortDesc
          ? "true"
          : "false";
      }
      return undefined;
    }
  }
})
export default class Search extends Vue {
  @Prop()
  query?: string;

  @Prop()
  sort?: string;

  request: SearchEntityRequest = {
    limit: 20,
    imLucky: true,
    scroll: true
  };

  profileStore: ProfileStore | null = null;

  result: SearchEntityResponse | null = null;
  groupOpen: boolean = true;

  get sortChoice(): string | undefined {
    return this.sort;
  }

  set sortChoice(value: string | undefined) {
    this.openSearchForQuery(this.query, value);
  }

  sortChoices: any = [
    {
      text: this.$app.translationService.t("SEARCH.GROUPSIZE_DOWN"),
      value: "filters.groupSize|true"
    },
    {
      text: this.$app.translationService.t("SEARCH.GROUPSIZE_UP"),
      value: "filters.groupSize|false"
    }
  ];

  async mounted(): Promise<void> {
    this.profileStore = this.$app.profileService.store;
    await this.refreshSearch();
  }

  @Watch("$route")
  async watchRoute(): Promise<void> {
    await this.refreshSearch();
  }

  async updateSport(sport: Sport): Promise<void> {
    await this.$app.webService.setPreferredSport(sport);
  }

  async refreshSearch(): Promise<void> {
    if (this.result) {
      this.result.scrollId = undefined;
      this.result.items = [];   
    }   
    await this.internalSearch();
  }  

  openSearchForQuery(query?: string, sort?: string) : void {
    this.$app.routerService.push({
      name: "Search",
      query: { q: query, s: sort }
    });
  }

  search(uri: string) : void {
    this.openSearchForQuery(uri, this.sortChoice);
  }

  async internalSearch(): Promise<void> {
    let scrollId = this.result?.scrollId ?? undefined;
    this.request.scrollId = scrollId;
    this.request.query = !scrollId ? this.query : undefined;
    this.request.sortField = this.sort ? this.sort.split("|")[0] : undefined;
    this.request.sortDesc = this.sort    
      ? this.sort.split("|")[1]
        ? this.sort.split("|")[1] === "true"
        : false
      : undefined;
    //this.request.filter = "type=SAMPLE.Sport";
    let hits = await this.$app.webService.search(this.request);
    if (hits && hits.items.length > 0) {
      if (!scrollId) {
        this.result = hits;
        if (this.result.query) {
          // this.request = this.result.query;
        }
      } else {
        if (this.result) {
          this.result.items.push(...hits.items);
        }
      }
    } else {
      if (!scrollId) {
        this.result = null;
      }
    }
  }
}
</script>