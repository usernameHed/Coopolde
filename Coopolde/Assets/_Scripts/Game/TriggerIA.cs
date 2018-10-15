using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerIA : MonoBehaviour
{
    [SerializeField]
    private MySelf mySelf;
    [SerializeField]
    private Me me;

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
                //lampManager.CentipedeInLamp(centi);
            }
        }
    }
}
