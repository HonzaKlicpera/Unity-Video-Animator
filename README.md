# Unity Video Animator
This repository contains a simple system for utilizing live-action animations in the video format. The animations were edited using a [custom Blender addon](https://github.com/HonzaKlicpera/Effective-footage-processing-Blender) and stylized using [EbSynth](https://ebsynth.com/).

## Project Structure
The components of the project can be found in `src/Assets`. The folders are structured as:
* **Art** – Contains all sprites and video files used in the demo.
* **Editor** – Contains imported NavMesh editor code. [source](https://github.com/Unity-Technologies/NavMeshComponents)
* **Prefabs** – Contains all prefabs used in the demo.
* **Resources** – Contains the movement velocity files.
* **Scenes** – Contains the demo game scene.
* **Scripts** – Contains the game scripts.

The `Scripts` folder further contains:
* **Movement** – Contains the movement component scripts.
* **NavMeshComponent** – Contains imported NavMesh high-level components. [source](https://github.com/Unity-Technologies/NavMeshComponents)
* **VideoAnimator** – Contains the video animator component scripts.
* **ClickManager.cs** – A helper class managing player click input.
* **PlayerCameraManager.cs** – Class controlling the camera movement using user input.
* **StateMachine.cs** – Class containing the base for state pattern.

## Game Demo
The demo game build can be downloaded from the latest release. Provided for Windows, Linux and Mac platforms (*Mac platform was not tested*).

### Controls
* **Camera** – The camera can be controlled by pressing down **left click** and **dragging the mouse pointer around** (like in Google maps). The camera zoom can be adjusted using the **mouse scroll wheel**. The camera can also be switched to follow the player by pressing the **F key**.
* **Player** – The player can be controlled by **left clicking** to any accessible place on the map.
* **Sitting character** – Can be commanded to sit up/down by pressing the **E key**.
