using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Me : PlayerController, IKillable
{
    [FoldoutGroup("GamePlay"), SerializeField]
    private float runningMultiplyer = 2;
    [FoldoutGroup("GamePlay"), SerializeField]
    private float timeOfDeath = 0.5f;
    [SerializeField]
    private GameObject spot;

    [SerializeField]
    private Vibration vibration;

    [SerializeField, ReadOnly]
    private bool isRunning = false;
    [SerializeField]
    private MySelf myself;

    private bool isDying = false;


    private void SetRunning(bool run)
    {
        SoundManager.GetSingleton.PlaySound("RobotRunStart", !run);
        if(!run == true) SoundManager.GetSingleton.PlaySound("RobotRunEnd");

        isRunning = run;
        spot.SetActive(!run);
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
        if (!enabledScript)
            return;
        base.Turn();
        InputMe();
    }

    [Button]
    public void Kill()
    {
        if (!enabledScript)
            return;

        enabledScript = false;

        StartCoroutine(RealyKill());
    }

    private IEnumerator RealyKill()
    {
        yield return new WaitForSeconds(timeOfDeath);


        ObjectsPooler.Instance.SpawnFromPool(GameData.PoolTag.ParticleBump, rb.transform.position, rb.transform.rotation, ObjectsPooler.Instance.transform);
        EventManager.TriggerEvent(GameData.Event.GameOver);
        PlayerConnected.Instance.SetVibrationPlayer(0, vibration);
        PlayerConnected.Instance.SetVibrationPlayer(1, vibration);

        CameraOrthoShake.Instance.CShake(2f, 1000f);
        //ScreenShake.Instance.ShakeCamera(2f, 0.5f);
        Destroy(rb.gameObject);
        playerMove.enabled = false;
        this.enabled = false;
    }
}
