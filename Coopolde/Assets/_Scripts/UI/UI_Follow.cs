using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Follow : MonoBehaviour 
{
    [SerializeField] private Transform target;


    private void FixedUpdate()
    {
        transform.LookAt(target);
    }
}
