using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// InputPlayer Description
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [FoldoutGroup("Object"), Tooltip("id unique du joueur correspondant à sa manette")]
    public EntityController pc;

    [FoldoutGroup("GamePlay")]
    public float speed = 300;

    [FoldoutGroup("GamePlay"), ReadOnly]
    public float speedMultiplyer = 1;

    public void Move(Vector3 dir, float factorSpeed = 1)
    {
        //Debug.Log("move !");
        UnityMovement.MoveByForcePushing_WithPhysics(pc.rb, dir, speed * factorSpeed * speedMultiplyer * Time.deltaTime);
    }
}
