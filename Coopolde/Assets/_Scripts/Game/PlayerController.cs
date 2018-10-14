using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingletonMono<PlayerController>
{
    [FoldoutGroup("GamePlay"), Tooltip("id unique du joueur correspondant à sa manette"), SerializeField]
    private int idPlayer = 0;
    public int IdPlayer { set { idPlayer = value; } get { return idPlayer; } }

    private bool enabledScript = true;      //tell if this script should be active or not

    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.GameOver, GameOver);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        enabledScript = true;               //active this script at start
    }

    /// <summary>
    /// move in physics, according to input of player
    /// </summary>
    private void MovePlayer()
    {

    }

    /// <summary>
    /// called when the game is over: desactive player
    /// </summary>
    private void GameOver()
    {
        Debug.Log("game over !!");
        enabledScript = false;
    }

    /// <summary>
    /// handle input
    /// </summary>
    private void Update()
    {
        if (!enabledScript)
            return;
    }

    /// <summary>
    /// handle move physics
    /// </summary>
    private void FixedUpdate()
    {
        if (!enabledScript)
            return;
        MovePlayer();
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameData.Event.GameOver, GameOver);
    }
}
