# bds-sample
Sample Plugins and Front End

The .net part can be build using vscode or visualstudio. .Net Core 3.1.102 is required. By default the output is in the home of BDS Server

The front part can be build using yarn command:
- yarn install
- yarn build or yarn serve
Check the config.json in ./public/configs/, to ensure the connection to your BDS server.

## Plugin

The plugin expose only one specific operation: /api/sample/v1/helloworld

## Front End

The front end is using the Bds Sdk for vue and vuetify.js for the material design provider.
