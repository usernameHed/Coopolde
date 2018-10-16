using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRedEyes : MonoBehaviour, IKillable
{
    [SerializeField]
    private GameObject refEyes;

    [SerializeField]
    private FrequencyCoolDown timer;

    private void Start()
    {
        timer.StartCoolDown();
    }

    private void Update()
    {
        if (timer.IsStartedAndOver())
        {
            GameObject redEyes = Instantiate(refEyes, transform.position, Quaternion.identity, transform.parent);
            Kill();
        }
    }

    public void GetHit(int amount, Vector3 posAttacker)
    {

    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
