using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonnus : MonoBehaviour
{
    [SerializeField, Range(0, 100)]
    private float percentToSet = 100;
    [SerializeField]
    private int difficultyToAdd = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(GameData.Layers.Player.ToString()))
        {
            CoopoldeManager.Instance.difficulty += difficultyToAdd;

            SoundManager.GetSingleton.PlaySound("Bonus");
            SpawnerBonus.Instance.Spawn();
            LightDecrease.Instance.SetToPercent(percentToSet);
            Destroy(gameObject);
        }
    }
}
