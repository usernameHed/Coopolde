using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLamp : MonoBehaviour
{
    [SerializeField]
    private MySelf lampManager;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GameData.Layers.Entity.ToString()))
        {
            CentipedeController centi = other.gameObject.GetComponentInParent<CentipedeController>();
            if (centi)
            {
                lampManager.CentipedeInLamp(centi);
            }
            
        }
    }
}
