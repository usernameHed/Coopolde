using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMe : MonoBehaviour
{
    [SerializeField]
    private CentipedeController CucaController;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GameData.Layers.Player.ToString()))
        {
            Debug.Log("ici player rentre");
            Me me = other.gameObject.GetComponentInParent<Me>();
            if (me)
            {
                CucaController.isMeInsideUs = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(GameData.Layers.Player.ToString()))
        {
            Debug.Log("ici player sort");
            Me me = other.gameObject.GetComponentInParent<Me>();
            if (me)
            {
                CucaController.isMeInsideUs = false;
            }
        }
    }
}
