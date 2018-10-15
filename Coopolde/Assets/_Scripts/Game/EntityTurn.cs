using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityTurn : MonoBehaviour
{
    [FoldoutGroup("GamePlay"), SerializeField]
    private float turnRate = 5f;

    [SerializeField]
    private Transform objectToTurn;

    [SerializeField]
    public bool canTurn = false;

    [SerializeField]
    private Vector2 dirToTurn;

    /// <summary>
    /// get la direction de l'arrow
    /// </summary>
    /// <returns></returns>
    public Vector2 GetDirArrow()
    {
        Vector2 dirArrowPlayer = ExtQuaternion.QuaternionToDir(objectToTurn.rotation, Vector3.up);
        return (dirArrowPlayer);
    }

    public void SetDirection(Vector2 dir, bool stop)
    {
        dirToTurn = dir;
        canTurn = !stop;
    }

    private void TurnArrow()
    {
        if (!canTurn)
            return;


        objectToTurn.rotation = ExtQuaternion.DirObject(
            objectToTurn.rotation,
            -dirToTurn.x,
            -dirToTurn.y,
            turnRate,
            ExtQuaternion.TurnType.Y);
            
    }

	// Update is called once per frame
	private void FixedUpdate ()
    {
        TurnArrow();
        //GetDirArrow();
    }
}
