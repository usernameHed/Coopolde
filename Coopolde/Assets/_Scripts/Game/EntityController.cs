using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityController : MonoBehaviour
{
    [FoldoutGroup("Entity"), Tooltip("ref move")]
    public PlayerMove playerMove;
    [FoldoutGroup("Entity"), Tooltip("ref turn")]
    public EntityTurn entityTurn;
    [FoldoutGroup("Entity"), Tooltip("rb")]
    public Rigidbody rb;

    protected bool enabledScript = true;      //tell if this script should be active or not

    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.GameOver, GameOver);
    }

    public virtual void Init()
    {
        enabledScript = true;               //active this script at start
    }

    /// <summary>
    /// move in physics, according to input of player
    /// </summary>
    protected void MoveEntity(Vector3 dir)
    {
        playerMove.Move(dir);
    }

    /// <summary>
    /// called when the game is over: desactive player
    /// </summary>
    public virtual void GameOver()
    {
        //Debug.Log("game over !!");
        enabledScript = false;
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameData.Event.GameOver, GameOver);
    }
}
