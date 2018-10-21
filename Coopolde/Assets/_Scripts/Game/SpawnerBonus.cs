using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBonus : SingletonMono<SpawnerBonus>
{
    [SerializeField]
    private float rangeRadius = 3f;

    [SerializeField]
    private Transform firstSpawn;

    [SerializeField]
    private List<IsOnCamera> spawnPoints;

    [SerializeField]
    private Transform parentSpawn;
    [SerializeField]
    private Transform parentBonus;
    [SerializeField]
    private GameObject prefabsBonus;

    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.GameOver, GameOver);
    }

    private void Start()
    {
        FirstSpawn();
    }

    public void Spawn()
    {
        
        List<IsOnCamera> spawns = new List<IsOnCamera>();
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (!spawnPoints[i].isOnScreen)
                spawns.Add(spawnPoints[i]);
        }
        int indexRandom = Random.Range(0, spawns.Count);
        Vector3 pos = spawns[indexRandom].transform.position;
        pos.x = (pos.x - rangeRadius) + Random.Range(0, rangeRadius * 2);
        pos.z = (pos.z - rangeRadius) + Random.Range(0, rangeRadius * 2);
        GameObject bonus = Instantiate(prefabsBonus, pos, Quaternion.identity, parentBonus);
    }

    public void FirstSpawn()
    {

        GameObject bonus = Instantiate(prefabsBonus, firstSpawn.position, Quaternion.identity, parentBonus);
    }

    public void GameOver()
    {
        Debug.Log("game over !!");
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameData.Event.GameOver, GameOver);
    }
}
