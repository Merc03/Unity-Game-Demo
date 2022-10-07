using UnityEngine;

static public class GlobalSettings {
    const float RotationSpeed = 6f;
    public const float RotationRadius = 17.5f;
    static System.TimeSpan time;
    private static float lastHeight;
    private static float scoreCount;

    public static void initialize() {
        lastHeight = 3f;
        scoreCount = 0f;
    }

    static void Update()
    {
        time = System.DateTime.Now.TimeOfDay;
    }

    /// <summary>
    /// Determines rotation degree by seconds
    /// </summary>
    /// <returns></returns>
    static public float getDegree() {
        Update();
        return (float)time.TotalSeconds * RotationSpeed * getMulti();
    }

    /// <summary>
    /// Generate a height based on last one
    /// </summary>
    /// <returns></returns>
    static public float nextHeight() {
        if(Random.value < 0.25f) {
            lastHeight =  Random.value * 3f + 0.25f;
        }
        else {
            lastHeight = myMath.nextGaussian(lastHeight, 3f, 0.25f, 3f);
        }
        return lastHeight;
    }

    public static float getTimeCount() {
        return myMath.nextGaussian(0.5f, 5f, 0.25f, 3f) / getMulti();
    }

    public static float getMulti() {
        if(scoreCount > 120f) {
            return 2f;
        }
        else {
            return 1f + scoreCount / 120f;
        }
    }
}

public static class myMath {

    private static float nextGaussian() {
        float v1, v2, s;
        do {
            v1 = 2f * Random.Range(0f, 1f) - 1f;
            v2 = 2f * Random.Range(0f, 1f) - 1f;
            s = v1 * v1 + v2 * v2;
        } while(s >= 1.0f || s <= 0f);

        s = Mathf.Sqrt((-2f * Mathf.Log(s)) / s);

        return v1 * s;
    }

    private static float nextGaussian(float mean, float standardDeviation) {
        return mean + nextGaussian() * standardDeviation;
    }

    public static float nextGaussian(float mean, float standardDeviation, float min, float max) {
        float x;

        do {
            x = nextGaussian(mean, standardDeviation);
        } while(x < min || x > max);

        return x;
    }



}