using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MySelf : PlayerController
{
    [SerializeField, ReadOnly]
    private bool lampActived = false;
    [SerializeField]
    private Me me;
    [SerializeField]
    private TriggerIA triggerLamp;
    [SerializeField]
    private Transform targetLose;
    [SerializeField]
    private Transform FollowSmooth;

    [SerializeField]
    private GameObject redLamp;
    [SerializeField]
    private GameObject whiteLamp;

    private bool gameIsOver = false;

    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.GameOver, GameOver);
    }

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
        gameIsOver = false;
        ActiveLamp(false);
    }

    /// <summary>
    /// active la lamp
    /// </summary>
    public void ActiveLamp(bool active)
    {
        lampActived = active;
        redLamp.SetActive(!lampActived);
        whiteLamp.SetActive(lampActived);
        triggerLamp.gameObject.SetActive(lampActived);
    }
    /*
    /// <summary>
    /// action when a centiped is in lamp
    /// </summary>
    public void CentipedeInLamp(CentipedeController centi)
    {
        Debug.Log("centiped inside MySelf");
    }*/

    private void InputMySelf()
    {
        

        if (me.playerInput.FireInput && !lampActived)
        {
            if (gameIsOver)
            {
                //ici on peut activer la lampe, mais je jeu est fini
                //TODO: dimiuer l'objet GameOver
                InitLocal.Instance.Previous();
                return;
            }

            ActiveLamp(true);
        }
        else if (lampActived && !me.playerInput.FireInput)
        {
            ActiveLamp(false);
        }
    }

    private void Update()
    {
        InputMySelf();
    }

    public override void GameOver()
    {
        Debug.Log("game over !!");
        enabledScript = false;
        gameIsOver = true;
        //FollowSmooth.SetParent(null);
        rb.transform.position = targetLose.position;
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameData.Event.GameOver, GameOver);
    }
}
