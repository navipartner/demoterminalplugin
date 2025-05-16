This repo contains an example of how to make a plugin .dll for the NP Hardware Connector.  
Follow the steps below to ensure you have a working plugin setup:

1. Reference https://www.nuget.org/packages/NPHardwareConnectorPlugin/ in your .NET project.

2. Make sure that the following settings are set in your .csproj file:

```
<EnableDynamicLoading>true</EnableDynamicLoading>

<ExcludeAssets>runtime</ExcludeAssets>
<Private>false</Private>
```

Just like in the demo project of this repository:

```
<Project Sdk="Microsoft.NET.Sdk">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <ImplicitUsings>enable</ImplicitUsings>
    <Nullable>enable</Nullable>
    <EnableDynamicLoading>true</EnableDynamicLoading>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="NPHardwareConnectorPlugin" Version="1.0.0">
      <ExcludeAssets>runtime</ExcludeAssets>
      <Private>false</Private>
    </PackageReference>
  </ItemGroup>

</Project>
```

3. Open /%localappdata%/NPHardwareConnector/plugins after installing and running the NP hardware connector at least once on your machine and create a folder inside it for each plugin you want to load. Inside your plugin-specific folder, put your .dll along with any dependencies it has.
   For the example project in this repo, we create a folder "DemoTerminal" and place the file DemoTerminalPlugin.dll inside it alongside the DemoTerminalPlugin.deps.json file:

```
/%localappdata%/NPHardwareConnector/plugins/DemoTerminal/DemoTerminalPlugin.dll
/%localappdata%/NPHardwareConnector/plugins/DemoTerminal/DemoTerminalPlugin.deps.json
```

4. Open settings.json in the /%localappdata%/NPHardwareConnector folder and ensure that the "PluginDlls" array contains a relative path to your plugin .dll.
   For the example project in this repo, we use the following settings.json:

```
{
  "AllowedUrls": [
    ".dynamics-retail.com",
    ".dynamics-retail.net",
    ".dynamics.com"
  ],
  "PluginDlls": ["DemoTerminal/DemoTerminalPlugin.dll"]
}
```

5. Now you should be good to go. This demo project uses handler name "DemoTerminal" meaning you can invoke it from POS Action javascript by the below code

```
_contextId = hwc.registerResponseHandler(async (hwcResponse) => {
  // Handle responses from HWC here
});

let requestJson = workflow.respond("getRequestJsonFromBC");
await hwc.invoke("DemoTerminal", requestJson, _contextId);
```

Note: This project has an IntegrationTests older that you can also use to confirm that everything works in the HWC, without developing anything in the POS yet.
But the tests will not run unless you add "null" to the "AllowedUrls" section of the settings.json file, to allow inbound http requests from a local .html file for testing purposes.
