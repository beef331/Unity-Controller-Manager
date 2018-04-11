# Unity-Controller-Manager
This is an attempt at a good Unity Controller Input Manager using the default Unity Input Manager. Currently it is setup to easily assign up to four controllers for
```diff
+Windows(Tested)
-Mac(Untested)
-Linux(Untested)
```
## How to use
Included is a tool for Windows that automates  adding the controller inputs that ControllerManager.cs has hardcoded in. This makes it easy to add controller support to any project. If not on a Windows platform one would either have to add them in the inspector of Unity or copy and paste the InputManager.asset file data from the resource folder into their own InputManager.
#### Windows
After building the WPF application from the InputManagerAdder, simply drop the .exe into the ProjectSettings folder of any Unity Project. Run it then decide which option you want. After running it, a backup is saved, but running it again will replace it.
Finally add the "ControllerManager.cs" file to the desired Unity project. The project is ready for controller support.

#### Other Platforms
Go to 
```
InputManagerAdder/InputManagerAdder/Resources
```
and open up InputManager.asset in your favourite text editor. Then go to the ProjectSettings folder in the project root directory. Open up the InputManager.asset and either replace it completely, or if you desire to keep old InputAxes skip copying
```
%YAML 1.1
%TAG !u! tag:unity3d.com,2011:
--- !u!13 &1
InputManager:
  m_ObjectHideFlags: 0
  serializedVersion: 2
  m_Axes:
```
and paste it at the bottom of the Unity Projects' InputManager.asset.
Finally add the "ControllerManager.cs" file to the desired Unity project. The project is ready for controller support.

#### Coding Doccumentation
First the Controller Namespace needs to be added in the script that intends to use the controllers.
```
using Controller;
```
After that Gamepad needs to be initialized with a integer between 1 and 4.
```
//This will give playerOne the controller Unity identifies is the first
GamePad playerOne = new GamePad(1)
```
For each controller the inputs either need to be read from a file path, or defaulted using the ResetToDefault function.
```
//This will give set playerOne to default values for your platform.
playerOne.ResetToDefault();
```

Then input can easily be got similar to Unity's default Input.Get system. Just like Unity there are 3 states a button can be in and a function to easily get them(Up,Down,Stay).
```
if(playerOne.GetAxis(InputAxis.LEFTX) > 0){
	//Do Something
}
```
## Credits
Programmed by : Jason Beetham

Offered under reuse with The MIT license.

