using System;
// ReSharper disable InconsistentNaming

[Flags]
public enum GameObjectLayer
{
    // builtin:
    Default = 1,
    TransparentFX = 1 << 1,
    IgnoreRaycast = 1 << 2,
    Water = 1 << 4,
    UI = 1 << 5,
    
    // custom:
    LayerComponent = 1 << 8
    
}
