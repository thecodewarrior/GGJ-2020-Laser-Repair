using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
// ReSharper disable InconsistentNaming

public static class GlobalUtils
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float3 vec(float x, float y, float z) => new float3(x, y, z);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float2 vec(float x, float y) => new float2(x, y);
}
