using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonnus : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameData.Layers.Player.ToString()))
        {
            SoundManager.GetSingleton.PlaySound("Bonus");
            SpawnerBonus.Instance.Spawn();
            Destroy(gameObject);
        }
    }
}
