using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentipedeController : EntityController, IKillable
{
    [FoldoutGroup("GamePlay"), SerializeField]
    private float magnitudeRun = 0.3f;
    [FoldoutGroup("GamePlay"), SerializeField]
    private float timeOfDeath = 0.5f;
    [Space(10)]
    [SerializeField, FoldoutGroup("GamePlay")]
    private float speedTurnOnWall = 10f;
    [SerializeField, FoldoutGroup("GamePlay")]
    private float speedMoveOnWall = 5f;

    [SerializeField]
    public GameObject refToFollow = null;

    [SerializeField]
    public IACentipede iaCentiped;

    [ReadOnly]
    public bool isAttacking = false;
    [ReadOnly]
    public bool isWalking = false;
    [ReadOnly]
    public bool isRunning = false;
    [ReadOnly]
    public bool isMeInsideUs = false;

    [SerializeField, ReadOnly]
    private bool isDying = false;
    public bool IsDying { get { return (isDying); } }


    private Vector3 dirCura = new Vector3(0, 0, 0);

    private void Start()
    {
        base.Init();
        Init();
        iaCentiped.Init();
    }

    public override void Init()
    {
        rb.transform.localPosition = Vector3.zero;
        isDying = false;

        refToFollow = CoopoldeManager.Instance.GetTarget();

        isAttacking = false;

        isWalking = false;
        isRunning = false;

        ChangeDirectionIA(new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
    }

    /// <summary>
    /// called by IA at each frame
    /// </summary>
    [Button]
    public void ChangeDirectionIA(Vector2 dir)
    {
        float oldMagnitude = dirCura.magnitude;
        dirCura = new Vector3(dir.x, dir.y, 0);
        float newMagnitude = dirCura.magnitude;

        if (newMagnitude >= magnitudeRun && oldMagnitude < magnitudeRun)
        {
            isWalking = true;
            isRunning = false;
        }
        else if (newMagnitude < magnitudeRun && oldMagnitude >= magnitudeRun)
        {
            isWalking = false;
            isRunning = true;
        }
        if (dir == Vector2.zero)
        {
            isWalking = false;
            isRunning = false;
        }
    }

    public void InvertDirection(Vector3 dir)
    {
        dirCura = dir * speedTurnOnWall;
        playerMove.Move(dirCura, speedMoveOnWall);
        //UnityMovement.MoveByForcePushing_WithPhysics(rb, dirCura, speedPlayer * GetOnlyForward() * speedMoveOnWall * Time.deltaTime);
    }

    /// <summary>
    /// set if the cuca is inside food or not, and if yes: set the reference
    /// </summary>
    public void SetAttacking(bool attack, GameObject toFollow)
    {
        isAttacking = attack;
        refToFollow = toFollow;
    }

    /// <summary>
    /// change direction of the script who turn
    /// </summary>
    private void TurnPlayer()
    {
        entityTurn.SetDirection(dirCura, false);
    }

    /// <summary>
    /// move in physics, according to input of player
    /// </summary>
    private void MovePlayer()
    {
        //don't move if getting angry
        if (iaCentiped.isGettingAngry)
            return;

        playerMove.Move(rb.transform.forward, dirCura.magnitude);
    }

    /// <summary>
    /// handle input
    /// </summary>
    private void Update()
    {
        if (!enabledScript)
            return;
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

    [Button]
    public void Kill()
    {
        if (isDying)
            return;

        isDying = true;
        entityTurn.SetDirection(dirCura, true);

        StartCoroutine(RealyKill());
    }

    public void GetHit(int hurt)
    {

    }

    private IEnumerator RealyKill()
    {
        yield return new WaitForSeconds(timeOfDeath);


        //ObjectsPooler.Instance.SpawnFromPool(GameData.PoolTag.DeadCuca, rb.transform.position, rb.transform.rotation, ObjectsPooler.Instance.transform);
        Destroy(gameObject);
    }
}
