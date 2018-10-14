using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysAtZero : MonoBehaviour
{
    [SerializeField]
    private Transform walls;

    private void FixedUpdate()
    {
        Vector3 posWall = walls.position;
        walls.position = new Vector3(posWall.x, 0f, posWall.z);
    }
}
