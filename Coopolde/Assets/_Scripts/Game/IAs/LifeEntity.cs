using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeEntity : MonoBehaviour
{
    [SerializeField]
    private GameObject objectKillable;
    [SerializeField]
    private bool shake = false;
    [SerializeField]
    public bool isPlayer = false;
    [SerializeField]
    private Vibration vibration;
    [SerializeField]
    private string soundToPlayHit;

    private IKillable killable;

    [SerializeField]
    private int lifeMax = 10;
    [SerializeField, ReadOnly]
    private int currentLife = 10;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        currentLife = lifeMax;
        killable = objectKillable.GetComponent<IKillable>();
        if (killable == null)
        {
            Debug.LogError("no killale");
        }
    }

    public void GetHit(int hurt, Vector3 posAttacker)
    {
        currentLife -= hurt;
        Debug.Log("hit ! -" + hurt);
        if (shake)
        {
            CameraOrthoShake.Instance.CShake(2f, 1000f);
            //            ScreenShake.Instance.ShakeCamera();
        }

        //son quand on est touché
        SoundManager.GetSingleton.PlaySound(soundToPlayHit);


        ObjectsPooler.Instance.SpawnFromPool(GameData.PoolTag.ParticleBump, transform.position, transform.rotation, ObjectsPooler.Instance.transform);
        PlayerConnected.Instance.SetVibrationPlayer(0, vibration);

        if (currentLife <= 0)
        {
            currentLife = 0;

            killable.Kill();
        }
        else
        {
            killable.GetHit(hurt, posAttacker);
        }

    }
}
