using UnityEngine;
using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Text;
using System.Collections.Generic;

/// <summary>
/// Fonctions utile
/// <summary>
public static class ExtRandom
{
    #region core script
    
    /// <summary>
    /// return an nams
    /// </summary>
    /// <returns></returns>
    public static string GetRandomName()
    {
        return (RandomNameGenerator.Instance.GetNames());
    }

    /// <summary>
    /// get random number between 2;
    /// </summary>
    public static int GetRandomNumber(int minimum, int maximum)
    {
        System.Random random = new System.Random();
        return random.Next() * (maximum - minimum) + minimum;
    }

    public static float GenerateNormalRandom(float mu, float sigma)
    {
        float rand1 = UnityEngine.Random.Range(0.0f, 1.0f);
        float rand2 = UnityEngine.Random.Range(0.0f, 1.0f);

        float n = Mathf.Sqrt(-2.0f * Mathf.Log(rand1)) * Mathf.Cos((2.0f * Mathf.PI) * rand2);

        return (mu + sigma * n);
    }
    #endregion
}
