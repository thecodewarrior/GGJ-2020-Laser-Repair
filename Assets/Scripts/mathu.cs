using System.Runtime.CompilerServices;
using Unity.Mathematics;
using static GlobalUtils;
// ReSharper disable InconsistentNaming

/// <summary>
/// mathu - math utilities
/// </summary>
public class mathu
{
    // using AggressiveInlining because the math package does it ¯\_(ツ)_/¯
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float round(float value, float stepSize) =>
        math.round(value / stepSize) * stepSize;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float2 round(float2 value, float2 stepSize) =>
        vec(round(value.x, stepSize.x), round(value.y, stepSize.y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float3 round(float3 value, float3 stepSize) =>
        vec(round(value.x, stepSize.x), round(value.y, stepSize.y), round(value.z, stepSize.z));
}
