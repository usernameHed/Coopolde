using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlwaysFollow : MonoBehaviour
{
    [FoldoutGroup("GamePlay"), Tooltip("Le smooth de la caméra"), SerializeField]
    private float smoothTime = 0.2f;

    [SerializeField]
    private Transform targetToFollow;

    private Vector3 currentVelocity;

    private void FixedUpdate()
    {
        // Move to desired position
        Vector3 desiredPosition = new Vector3(targetToFollow.position.x, transform.position.y, targetToFollow.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothTime);
        //posLisener = transform.position;    //change listenerPosition
    }
}
