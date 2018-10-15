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
    private GameObject redLamp;
    [SerializeField]
    private GameObject whiteLamp;

    private void Start()
    {
        Init();
    }

    public override void Init()
    {
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
}
