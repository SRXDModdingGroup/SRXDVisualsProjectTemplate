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
- Duplicate the VisualsSceneTemplate prefab and place it in your new folder. Name it Scene
- Open your scene prefab and add visual objects under the Visuals gameObject
- Place an instance of your scene prefab into the scene hierarchy. Ensure that the instance's transform is all 0 for position and rotation, and all 1 for scale
- Press Play to enter the game view and see a preview of how the scene will appear in-game

### Adding Visuals Controllers

- Visuals controllers are scripts that can listen for events and properties in order to synchronize visual effects with gameplay
- To add a new controller, create an empty gameObject under the Controllers gameObject in your scene prefab
- Attach a script that derives from VisualsController to this gameObject (a few controller scripts are provided by SRXDCustomVisuals.Behaviors and have names ending with "Controller")
- Assign target visuals to this controller and tweak its parameters to your liking
- Go to the VisualElement script at the root of your scene prefab and add a new mapping for the event or property that you want to access the new controller
- Assign your controller to the mapping's Target. Enter the name of the controller action or property that you want the mapping to control
- Press Play to enter the game view. Click the buttons and toggles on the left side to simulate in-game events and properties

### Building AssetBundles and Adding Them to SRXD

- Right click inside the folder containing your scene prefab and select Create > Visuals Module, name it Module
- Add an element entry to the module and assign your scene prefab to it. Set Root to 0 if you want the scene to be anchored to the world in-game, or 1 if you want it to be anchored to the camera (1 is recommended)
- Assign an asset bundle to both your scene and your module (at the bottom of the Inspector)
- Open the AssetBundle Browser (Window > AssetBundleBrowser) and click Build
- All AssetBundles will be built to
  - ```<Project Folder>/AssetBundles/StandaloneWindows```
- Copy both the asset bundle file and manifest file for each bundle to the AssetBundles folder in your customs folder
- Create a new .json file in the Backgrounds folder in your customs folder. Add the following (you may omit the reference to the "common" bundle if you did not use any assets from it):

```json
{
	"disableBaseBackground": true,
	"assetBundles": [
		"<Bundle Name>",
		"common"
	],
	"modules": [
		{
			"bundle": "<Bundle Name>",
			"asset": "Module"
		}
	]
}
```
