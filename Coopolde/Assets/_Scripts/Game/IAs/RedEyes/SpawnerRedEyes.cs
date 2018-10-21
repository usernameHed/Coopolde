using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerRedEyes : MonoBehaviour
{
    [SerializeField]
    private float minTime = 2;
    [SerializeField]
    private float maxTime = 10;
    [SerializeField]
    private float rangeRadius = 3f;

    [SerializeField]
    private GameObject prefabsRedEyes;

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
        Vector2 pos = new Vector2(Random.Range(0, Screen.width), Random.Range(0, Screen.height));
        Vector3 realPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, GameManager.Instance.CameraMain.transform.position.y));
        GameObject redEyes = Instantiate(prefabsRedEyes, realPos, Quaternion.identity, transform);
        redEyes.transform.localPosition = new Vector3(redEyes.transform.localPosition.x, 0, redEyes.transform.localPosition.z);
    }

    private void Update()
    {
        if (timerSpawn.IsStartedAndOver())
        {
            Spawn();
            timerSpawn.Reset();
            timerSpawn.StartCoolDown(Random.Range(minTime / CoopoldeManager.Instance.difficulty, maxTime / CoopoldeManager.Instance.difficulty));
            //Debug.Log("Spawner !!");
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
