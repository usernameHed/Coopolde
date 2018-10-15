using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEntity : MonoBehaviour
{
    [SerializeField]
    private int hurtAmount = 1;
    [SerializeField]
    private bool hurtOnlyPlayer = false;

    [SerializeField]
    private FrequencyCoolDown coolDown;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        coolDown.Reset();
    }

    private void Hurt(GameObject other)
    {
        LifeEntity life = other.GetComponent<LifeEntity>();
        if (life)
        {
            if (!life.isPlayer && hurtOnlyPlayer)
                return;

            coolDown.StartCoolDown();
            life.Hit(hurtAmount);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GameData.Layers.Hurt.ToString())
            && coolDown.IsReady())
        {
            Hurt(other.gameObject);
        }
    }
}
