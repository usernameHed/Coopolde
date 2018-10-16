using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerIA : MonoBehaviour
{
    [SerializeField]
    private MySelf mySelf;
    [SerializeField]
    private Me me;
    [SerializeField]
    private bool killEye = true;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GameData.Layers.Entity.ToString()))
        {
            CentipedeController centi = other.gameObject.GetComponentInParent<CentipedeController>();
            if (centi)
            {
                /*
                if (mySelf)
                    mySelf.CentipedeInLamp(centi);
                if (me)
                    me.CentipedeInLamp(centi);
                */
                if (centi.iaCentiped.IsPasiveAndReadyToGetAngry())
                {
                    centi.isAttacking = true;
                    Debug.Log(centi.gameObject.name);
                }
                return;
                //lampManager.CentipedeInLamp(centi);
            }
            RedEyes redEye = other.gameObject.GetComponentInParent<RedEyes>();
            if (redEye && killEye)
            {
                redEye.TryToKill();
                return;
            }
            /*
            SpawnRedEyes spawn = other.gameObject.GetComponentInParent<SpawnRedEyes>();
            if (spawn)
            {
                spawn.Kill();
            }
            */
        }
    }
}
