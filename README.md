# bds-sample
Sample Plugins and Front End

The .net part can be build using vscode or visualstudio. .Net Core 3.1.102 is required. By default the output is in the home of BDS Server
- dotnet build
- docker restart bds (To ensure the server will load this new plugin)

The front part can be build using yarn command:
- cd ./front
- yarn install
- yarn build or yarn serve
Check the config.json in ./public/configs/, to ensure the connection to your BDS server.

By using Visual Studio Code you cn easily debug your plugin running in the container. Open vscode, open the folder bds-sample-source, hit F5, select the process dotnet in the container. You can put a breackpoint where you need.

## Plugin

The plugin is defining a new Client Application named SAMPLE.
The plugin will create an API User for this client application.
The plugin expose the operations:
- /api/sample/v1/helloworld : Return an hello world message

## Front End

The front end is using the Bds Sdk for vue and vuetify.js for the material design provider.
