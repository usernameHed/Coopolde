using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerCrawler : MonoBehaviour
{
    [SerializeField]
    private float minTime = 2;
    [SerializeField]
    private float maxTime = 10;

    [SerializeField]
    private List<IsOnCamera> spawnPoints;

    [SerializeField]
    private Transform parentSpawn;
    [SerializeField]
    private Transform parentCrawlers;
    [SerializeField]
    private GameObject prefabsCrawler;

    private FrequencyCoolDown timerSpawn = new FrequencyCoolDown();

    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.GameOver, GameOver);
    }

    private void Start()
    {
        timerSpawn.StartCoolDown(Random.Range(minTime, maxTime));
    }

    private void Spawn()
    {
        List<IsOnCamera> spawns = new List<IsOnCamera>();
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            if (!spawnPoints[i].isOnScreen)
                spawns.Add(spawnPoints[i]);
        }
        int indexRandom = Random.Range(0, spawns.Count);
        GameObject Crawler = Instantiate(prefabsCrawler, spawns[indexRandom].transform.position, Quaternion.identity, parentCrawlers);
    }

    private void Update()
    {
        if (timerSpawn.IsStartedAndOver())
        {
            Spawn();
            timerSpawn.Reset();
            timerSpawn.StartCoolDown(Random.Range(minTime, maxTime));
            Debug.Log("Spawner !!");
        }
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
