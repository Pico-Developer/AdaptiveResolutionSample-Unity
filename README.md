Adaptive Resolution Sample shows how to use adaptive resolution to dynamically adjust resolution based on GPU load. This can be used to automatically increase quality (by increasing resolution) when GPU load is low. It can also help maintain stable FPS (by lowering resolution) when GPU load is high.


## Development environment
- Unity Editor: 2021.3.27f1
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


