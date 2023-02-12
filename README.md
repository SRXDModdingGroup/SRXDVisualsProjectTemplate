# SRXDVisualsProjectTemplate
A Unity project template for creating custom visuals.

### Getting Started

- Install Unity Hub and Unity version 2022.1.12
- Download the latest release of srxd.visuals.tgz
- Place the file in the Unity projects template folder at
  - ```C:\Program Files\Unity\Hub\Editor\2022.1.12f1\Editor\Data\Resources\PackageManager\ProjectTemplates```
- Restart Unity Hub and create a new Unity project using this template
- Open the new project

### Creating a New Visual Element

- Create a new folder in the Bundles folder to contain the assets for your visual element
- Create a new folder in the Scripts folder to contain any scripts used by your visual element. Add an assembly definition to this folder
- Create a new GameObject in your scene. Ensure that the prefab's transform is all 0 for position and rotation, and all 1 for scale
- Add the VisualsEventReceiver script to your GameObject
- Drag the GameObject into your new assets folder to create a prefab
- After adding visuals to your prefab, press Play to enter the game view and see a preview of how the scene will appear in-game

### Building AssetBundles and Adding Them to SRXD

- Select your prefab asset and assign an asset bundle at the bottom of the Inspector
- Open the AssetBundle Browser (Window > AssetBundle Browser) and click Build
- By default, all AssetBundles will be built to
  - ```<Project Folder>/AssetBundles/StandaloneWindows```
- Copy both the asset bundle file and manifest file for each bundle to the AssetBundles folder in your plugins folder
- Create a new .json file in the Backgrounds folder in your plugins folder. Add the following:

```json
{
	"disableBaseBackground": true,
	"assetBundles": [
		"<Bundle Name>",
		"common"
	],
	"assemblies": [
		"<Assembly Name>",
		"SRXDBackgrounds.Common.dll"
	],
	"elements": [
		{
			"bundle": "<Bundle Name>",
			"asset": "<Prefab Name>",
			"root": 1
		}
	]
}
```
