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

    [SerializeField]
    private Transform fallBackTarget;

    private bool gameOver = false;

    private Vector3 currentVelocity;

    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.GameOver, GameOver);
        gameOver = false;
    }

    private void GameOver()
    {
        gameOver = true;
    }

    private void FixedUpdate()
    {
        Vector3 desiredPosition;
        if (!gameOver)
            desiredPosition = new Vector3(targetToFollow.position.x, transform.position.y, targetToFollow.position.z);
        else
            desiredPosition = new Vector3(fallBackTarget.position.x, transform.position.y, fallBackTarget.position.z);
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothTime);
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameData.Event.GameOver, GameOver);
    }
}
