Adaptive Resolution Demo and Sample shows how to use adaptive resolution to dynamically adjust resolution based on GPU load. This can be used to automatically increase quality (by increasing resolution) when GPU load is low. It can also help maintain stable FPS (by lowering resolution) when GPU load is high.

## Scenes
1. Demo: Show Sci-fi game style scenes with rooms of different pixel complexity. Adaptive resolution automatically adjusts resolution based on GPU load of current view direction.

2. Samples\AdaptiveResolutionTest: Shows a simple cubes scene with high and low complexity areas where adaptive resolution will automatically adjust resolution based on GPU load. 
 
## Development environment
- Unity Editor: 2022.3.2f1 or above
- PICO Unity Integration SDK: 2.3.0
- PICO Device: PICO 4
- PICO Device's System Version: 5.7.0
- Graphics API: Vulkan or OPENGL
- Input System: 1.6.1
- Rendering Pipeline: Built-in Renderer and URP
- XR Interaction Toolkit: 2.3.2
- XR Plugin Management: 4.3.3

## Project settings
| Item | Setting | 
| --- | --- |
| Stereo Rendering Mode | Multiview |
| Adaptive Resolution | On |
| MSAA | 4x |
| Number of light sources | 1x Directional Light |
| HDR | Off |
| Graphics API | Vulkan | OpenGL


## Optimization methods
- Multiview rendering
- Adaptive Resolution


