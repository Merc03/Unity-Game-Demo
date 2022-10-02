using System;
using UnityEngine;

public class globalSettings : MonoBehaviour
{
    const float globalRotationSpeed = 30f;

    TimeSpan time;

    void Awake()
    {
        time = DateTime.Now.TimeOfDay;
    }

    // Update is called once per frame
    void Update()
    {
        time = DateTime.Now.TimeOfDay;
    }

    /// <summary>
    /// Determines rotation degree by seconds
    /// </summary>
    /// <returns></returns>
    public float getDegree() {
        Update();
        time = DateTime.Now.TimeOfDay;
        return (float)time.TotalSeconds * globalRotationSpeed;
    }
}
