using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// InputPlayer Description
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [FoldoutGroup("GamePlay")]
    public float speed = 300;


    [FoldoutGroup("Object"), Tooltip("id unique du joueur correspondant à sa manette")]
    public PlayerController pc;


    public void Move()
    {
        //Debug.Log("move !");
        UnityMovement.MoveByForcePushing_WithPhysics(pc.rb, pc.playerInput.GetDirInput(), speed * Time.deltaTime);
    }
}
