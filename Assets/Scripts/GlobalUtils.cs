using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
// ReSharper disable InconsistentNaming

public static class GlobalUtils
{
    // using AggressiveInlining because the math package does it ¯\_(ツ)_/¯
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float3 vec(float x, float y, float z) => new float3(x, y, z);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float2 vec(float x, float y) => new float2(x, y);
    
    /// <summary>
    /// Tests whether this object is in a prefab editor scene. Useful to avoid modifying prefabs with custom editor code.
    /// </summary>
    /// <returns>Returns true if this GameObject is in a prefab editor scene.</returns>
    public static bool IsInPrefabScene(this GameObject gameObject)
    {
        return 
            gameObject.scene.name != "" && // In a prefab the scene name is the name of the prefab file
            gameObject.scene.path == ""; // but the scene doesn't actually have a path
    }
}
