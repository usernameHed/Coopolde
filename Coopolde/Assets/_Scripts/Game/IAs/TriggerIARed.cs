using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerIARed : MonoBehaviour
{
    [SerializeField]
    private MySelf mySelf;
    [SerializeField]
    private Me me;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(GameData.Layers.RedEye.ToString()))
        {
            SpawnRedEyes spawn = other.gameObject.GetComponentInParent<SpawnRedEyes>();
            if (spawn)
            {
                spawn.Kill();
            }
        }
    }
}
