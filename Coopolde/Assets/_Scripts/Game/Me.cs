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
    [FoldoutGroup("GamePlay"), SerializeField]
    private float factorRepulseHit = 5f;

    [SerializeField]
    private Material whiteMaterial;
    [SerializeField]
    private Material redMaterial;
    [SerializeField]
    private List<MeshRenderer> meshToChangeColor;

    [SerializeField]
    private GameObject spot;

    [SerializeField]
    private Vibration vibration;

    [SerializeField]
    private FrequencyCoolDown timeToStayRed;

    [SerializeField, ReadOnly]
    private bool isRunning = false;
    [SerializeField]
    private MySelf myself;

    private bool isDying = false;
    private bool gameOver = false;

    private void Awake()
    {
        myself = CoopoldeManager.Instance.mySelf;
    }


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

        if (timeToStayRed.IsStartedAndOver())
        {
            SetMaterials(true);
        }
    }

    [Button]
    public void Kill()
    {
        if (!enabledScript)
            return;

        enabledScript = false;

        //TODO: son quand lle joueur meurt
        SoundManager.GetSingleton.PlaySound("PlayerDie");

        StartCoroutine(RealyKill());
    }

    public void GetHit(int hurt, Vector3 posAttacker)
    {
        Vector3 dir = rb.transform.position - posAttacker;
        playerMove.Move(dir, factorRepulseHit);

        timeToStayRed.StartCoolDown();
        SetMaterials(false);
    }

    private void SetMaterials(bool white)
    {
        for (int i = 0; i < meshToChangeColor.Count; i++)
        {
            meshToChangeColor[i].material = (white) ? whiteMaterial : redMaterial;
        }
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
