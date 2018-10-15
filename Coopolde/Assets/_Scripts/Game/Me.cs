using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Me : PlayerController
{
    [FoldoutGroup("GamePlay"), SerializeField]
    private float runningMultiplyer = 2;

    [SerializeField, ReadOnly]
    private bool isRunning = false;
    [SerializeField]
    private MySelf myself;


    private void SetRunning(bool run)
    {
        isRunning = run;
        playerMove.speedMultiplyer = (isRunning) ? runningMultiplyer : 1;
    }

    private void InputMe()
    {
        if (myself.playerInput.FireInput && !isRunning)
        {
            SetRunning(true);
        }
        else if (isRunning && !myself.playerInput.FireInput)
        {
            SetRunning(false);
        }
    }
    /*
    /// <summary>
    /// action when a centiped is in lamp
    /// </summary>
    public void CentipedeInLamp(CentipedeController centi)
    {
        Debug.Log("centiped inside Me");
    }
    */

    private void Update()
    {
        base.Turn();
        InputMe();
    }
}
