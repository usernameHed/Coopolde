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
    private float rangeRadius = 3f;

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
        Vector3 pos = spawns[indexRandom].transform.position;
        pos.x = (pos.x - rangeRadius) + Random.Range(0, rangeRadius * 2);
        pos.z = (pos.z - rangeRadius) + Random.Range(0, rangeRadius * 2);
        GameObject Crawler = Instantiate(prefabsCrawler, pos, Quaternion.identity, parentCrawlers);
    }

    private void Update()
    {
        if (timerSpawn.IsStartedAndOver())
        {
            Spawn();
            timerSpawn.Reset();
            timerSpawn.StartCoolDown(Random.Range(minTime / CoopoldeManager.Instance.difficulty, maxTime / CoopoldeManager.Instance.difficulty));
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
