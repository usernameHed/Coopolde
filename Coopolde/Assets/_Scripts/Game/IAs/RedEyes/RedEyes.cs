using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEyes : EntityController, IKillable
{
    [FoldoutGroup("GamePlay"), SerializeField]
    private float timeOfDeath = 0.5f;

    [FoldoutGroup("GamePlay"), SerializeField]
    private IsOnCamera isOnCamera;

    [FoldoutGroup("GamePlay"), SerializeField]
    private GameObject nice;
    [FoldoutGroup("GamePlay"), SerializeField]
    private GameObject bad;
    [FoldoutGroup("GamePlay"), SerializeField]
    private FrequencyCoolDown timerToGetAngry;

    private bool isAngry = false;

    [ReadOnly]
    public GameObject refToFollow = null;

    [SerializeField, ReadOnly]
    private bool isDying = false;
    public bool IsDying { get { return (isDying); } }

    private Vector3 dirCura = new Vector3(0, 0, 0);


    private void Start()
    {
        base.Init();
        Init();
    }

    public override void Init()
    {
        refToFollow = CoopoldeManager.Instance.GetTarget();

        rb.transform.localPosition = Vector3.zero;
        isDying = false;
        GetNice();
    }

    private void SetAngry()
    {
        if (timerToGetAngry.IsWaiting() && !isOnCamera.isOnScreen && !timerToGetAngry.IsOnPause())
        {
            Debug.Log("pause timer at: " + timerToGetAngry.GetTimer());
            timerToGetAngry.PauseTimer();
        }
        if (timerToGetAngry.IsOnPause() && isOnCamera.isOnScreen)
        {
            timerToGetAngry.PauseEnd();
            Debug.Log("pause end at: " + timerToGetAngry.GetTimer());
        }
        /*if (timerToGetAngry.IsWaiting() && !isOnCamera.isOnScreen && !isAngry)
        {
            Debug.Log("pause timer at: " + timerToGetAngry.GetTimer());
            timerToGetAngry.PauseTimer();
        }
        else if (timerToGetAngry.IsWaiting() && isOnCamera.isOnScreen && !isAngry)
        {
            timerToGetAngry.PauseEnd();
            Debug.Log("pause end at: " + timerToGetAngry.GetTimer());
        }*/

        if (timerToGetAngry.IsStartedAndOver())
        {
            isAngry = true;
            nice.SetActive(false);
            bad.SetActive(true);
        }
    }

    private void GetNice()
    {
        nice.SetActive(true);
        bad.SetActive(false);
        isAngry = false;
        timerToGetAngry.StartCoolDown();
    }

    /// <summary>
    /// change direction of the script who turn
    /// </summary>
    private void TurnPlayer()
    {
        if (!refToFollow)
            return;

        dirCura = refToFollow.transform.position - rb.transform.position;
        if (entityTurn)
            entityTurn.SetDirection(new Vector2(dirCura.x, dirCura.z), false);
    }

    /// <summary>
    /// move in physics, according to input of player
    /// </summary>
    private void MovePlayer()
    {
        if (!isAngry)
            return;
        playerMove.Move(dirCura);
    }

    /// <summary>
    /// handle input
    /// </summary>
    private void Update()
    {
        if (!enabledScript)
            return;
        SetAngry();
        TurnPlayer();
    }

    /// <summary>
    /// handle move physics
    /// </summary>
    private void FixedUpdate()
    {
        if (!enabledScript || isDying)
            return;
        MovePlayer();
    }

    public void TryToKill()
    {
        if (!isAngry)
            Kill();
    }

    [Button]
    public void Kill()
    {
        if (isDying)
            return;

        isDying = true;
        if (entityTurn)
            entityTurn.SetDirection(dirCura, true);

        //TODO: son quand lle joueur meurt
        SoundManager.GetSingleton.PlaySound("RedEyesDie");
        Destroy(gameObject);
        //StartCoroutine(RealyKill());
    }

    public void GetHit(int hurt)
    {
        //stunTime.StartCoolDown();
    }
    /*
    private IEnumerator RealyKill()
    {
        yield return new WaitForSeconds(timeOfDeath);


        //ObjectsPooler.Instance.SpawnFromPool(GameData.PoolTag.DeadCuca, rb.transform.position, rb.transform.rotation, ObjectsPooler.Instance.transform);
        Destroy(gameObject);
    }
    */
}
