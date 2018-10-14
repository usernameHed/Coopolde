using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnLamp : MonoBehaviour
{
    [SerializeField]
    private float turnRate = 5f;

    [SerializeField]
    private Transform dirLamp;

    [FoldoutGroup("Object"), Tooltip("id unique du joueur correspondant à sa manette")]
    public PlayerController pc;

    /// <summary>
    /// get la direction de l'arrow
    /// </summary>
    /// <returns></returns>
    public Vector3 GetDirArrow()
    {
        Vector3 dirArrowPlayer = ExtQuaternion.QuaternionToDir(dirLamp.rotation, Vector3.up);
        //Debug.DrawRay(transform.position, dirArrowPlayer.normalized, Color.yellow, 1f);
        return (dirArrowPlayer);
    }

    private void TurnArrow()
    {
        if (pc.playerInput.NotMoving())
            return;

        dirLamp.rotation = ExtQuaternion.DirObject(
            dirLamp.rotation,
            -pc.playerInput.Horiz,
            -pc.playerInput.Verti,
            turnRate,
            ExtQuaternion.TurnType.Y);
    }

	// Update is called once per frame
	private void Update ()
    {
        TurnArrow();
        GetDirArrow();
    }
}
