using System;
using UnityEngine;

static public class GlobalSettings
{
    const float RotationSpeed = 6f;
    public const float RotationLocationY = -17.5f;
    static TimeSpan time;
    public static int maxParticle = 30;

    static void Update()
    {
        time = DateTime.Now.TimeOfDay;
    }

    /// <summary>
    /// Determines rotation degree by seconds
    /// </summary>
    /// <returns></returns>
    static public float getDegree() {
        Update();
        time = DateTime.Now.TimeOfDay;
        return (float)time.TotalSeconds * RotationSpeed;
    }
}
