﻿using UnityEngine;
using Sirenix.OdinInspector;

/// <summary>
/// InputPlayer Description
/// </summary>
public class PlayerInput : MonoBehaviour
{
    [FoldoutGroup("Object"), Tooltip("id unique du joueur correspondant à sa manette"), SerializeField]
    private PlayerController playerController;
    public PlayerController PlayerController { get { return (playerController); } }

    private float horiz;    //input horiz
    public float Horiz { get { return (horiz); } }
    private float verti;    //input verti
    public float Verti { get { return (verti); } }
    private bool fireA; //jump input
    public bool FireInput { get { return (fireA); } }
    private bool fireUpA; //jump input
    public bool FireUpInput { get { return (fireUpA); } }

    private bool gripInput; //grip input hold
    public bool GripInput { get { return (gripInput); } }
    private bool gripDownInput; //grgip input down
    public bool GripDownInput { get { return (gripDownInput); } }
    private bool gripUpInput; //grip input up
    public bool GripUpInput { get { return (gripUpInput); } }

    private bool fatInput; //grip input hold
    public bool FatInput { get { return (fatInput); } }
    private bool fatDownInput; //grgip input down
    public bool FatDownInput { get { return (fatDownInput); } }
    private bool fatUpInput; //grip input up
    public bool FatUpInput { get { return (fatUpInput); } }

    private float modyfyRopeAddDownInput; //rope add
    public float ModyfyRopeAddDownInput { get { return (modyfyRopeAddDownInput); } }
    private float modyfyRopeRemoveDownInput; //rope remove
    public float ModyfyRopeRemoveDownInput { get { return (modyfyRopeRemoveDownInput); } }

    /// <summary>
    /// get la direction de l'input
    /// </summary>
    /// <returns></returns>
    public Vector3 GetDirInput()
    {
        //Vector3 dirArrowPlayer = QuaternionExt.QuaternionToDir(dirArrow.rotation, Vector3.up);
        Vector3 dirInputPlayer = new Vector3(horiz, 0, verti);
        //Debug.DrawRay(transform.position, dirInputPlayer.normalized, Color.yellow, 1f);
        return (dirInputPlayer);
    }

    /// <summary>
    /// retourne si le joueur se déplace ou pas
    /// </summary>
    /// <returns></returns>
    public bool NotMoving()
    {
        if (horiz == 0 && verti == 0)
            return (true);
        return (false);
    }

    /// <summary>
    /// tout les input du jeu, à chaque update
    /// </summary>
    private void GetInput()
    {
        horiz = PlayerConnected.Instance.GetPlayer(playerController.idPlayer).GetAxis("Move Horizontal");
        verti = PlayerConnected.Instance.GetPlayer(playerController.idPlayer).GetAxis("Move Vertical");

        fireA = PlayerConnected.Instance.GetPlayer(playerController.idPlayer).GetButton("FireA");
        fireUpA = PlayerConnected.Instance.GetPlayer(playerController.idPlayer).GetButtonUp("FireA");

        gripInput = PlayerConnected.Instance.GetPlayer(playerController.idPlayer).GetButton("FireX") || PlayerConnected.Instance.GetPlayer(playerController.idPlayer).GetButton("FireY");
        gripUpInput = PlayerConnected.Instance.GetPlayer(playerController.idPlayer).GetButtonUp("FireX") || PlayerConnected.Instance.GetPlayer(playerController.idPlayer).GetButtonUp("FireY");
        gripDownInput = PlayerConnected.Instance.GetPlayer(playerController.idPlayer).GetButtonDown("FireX") || PlayerConnected.Instance.GetPlayer(playerController.idPlayer).GetButtonDown("FireY");

        fatInput = PlayerConnected.Instance.GetPlayer(playerController.idPlayer).GetButton("FireY");
        fatUpInput = PlayerConnected.Instance.GetPlayer(playerController.idPlayer).GetButtonUp("FireY");
        fatDownInput = PlayerConnected.Instance.GetPlayer(playerController.idPlayer).GetButtonDown("FireY");

        modyfyRopeAddDownInput = Mathf.Clamp((PlayerConnected.Instance.GetPlayer(playerController.idPlayer).GetAxis("LeftTrigger2") + PlayerConnected.Instance.GetPlayer(playerController.idPlayer).GetAxis("RightTrigger1")), 0f, 1f);
        modyfyRopeRemoveDownInput = Mathf.Clamp((PlayerConnected.Instance.GetPlayer(playerController.idPlayer).GetAxis("RightTrigger2") + PlayerConnected.Instance.GetPlayer(playerController.idPlayer).GetAxis("LeftTrigger1")), 0f, 1f);
    }

    private void Update()
    {
        GetInput();
    }
}
