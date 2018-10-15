using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : EntityController
{
    [FoldoutGroup("Object"), Tooltip("id unique du joueur correspondant à sa manette")]
    public PlayerInput playerInput;
    

    [FoldoutGroup("GamePlay"), Tooltip("id unique du joueur correspondant à sa manette"), SerializeField]
    public int idPlayer = 0;

    
    private void Start()
    {
        base.Init();
    }

    /// <summary>
    /// move in physics, according to input of player
    /// </summary>
    private void MovePlayer()
    {
        MoveEntity(playerInput.GetDirInput());
    }

    protected void Turn()
    {
        if (!enabledScript)
            return;
        Vector3 input = playerInput.GetDirInput();
        bool stop = playerInput.NotMoving();
        if (entityTurn)
            entityTurn.SetDirection(new Vector2(input.x, input.z), stop);
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
}
