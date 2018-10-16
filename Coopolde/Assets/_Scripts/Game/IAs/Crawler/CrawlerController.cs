using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerController : EntityController, IKillable
{
    [FoldoutGroup("GamePlay"), SerializeField]
    private float timeOfDeath = 0.5f;

    [FoldoutGroup("GamePlay"), SerializeField]
    private FrequencyCoolDown stunTime;

    [SerializeField]
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
    }

    /// <summary>
    /// change direction of the script who turn
    /// </summary>
    private void TurnPlayer()
    {
        if (!refToFollow)
            return;

        dirCura = refToFollow.transform.position - rb.transform.position;
        entityTurn.SetDirection(new Vector2(dirCura.x, dirCura.z), false);
    }

    /// <summary>
    /// move in physics, according to input of player
    /// </summary>
    private void MovePlayer()
    {
        playerMove.Move(rb.transform.forward);
    }

    /// <summary>
    /// handle input
    /// </summary>
    private void Update()
    {
        if (!enabledScript || !stunTime.IsReady())
            return;
        TurnPlayer();
    }

    /// <summary>
    /// handle move physics
    /// </summary>
    private void FixedUpdate()
    {
        if (!enabledScript || isDying || !stunTime.IsReady())
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
        stunTime.StartCoolDown();
    }

    private IEnumerator RealyKill()
    {
        yield return new WaitForSeconds(timeOfDeath);


        //ObjectsPooler.Instance.SpawnFromPool(GameData.PoolTag.DeadCuca, rb.transform.position, rb.transform.rotation, ObjectsPooler.Instance.transform);
        Destroy(gameObject);
    }
}
