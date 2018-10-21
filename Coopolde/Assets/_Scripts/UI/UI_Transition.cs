using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Transition
{
    PLAY,
    CREDITS,
    QUIT
}

public class UI_Transition : MonoBehaviour 
{
    [SerializeField] private Animation anim;
    [SerializeField] private Transition whenSelected;

    [SerializeField] private float end = 0.8f;
    [SerializeField] private float speed = 1;





    private bool isSelecting;


    void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<UI_Controller>().lightState != isSelecting)
        {
            Select(other.GetComponent<UI_Controller>().lightState);
            Debug.Log(isSelecting);
        }
    }

    void OnTriggerExit(Collider other)
    {
        Select(false);
    }

    private void Start()
    {
        anim["TextSelect"].speed = 0.0f;
    }

    private void Update()
    {
        if(anim["TextSelect"].time < 0.1f) anim["TextSelect"].time = 0.1f;
        if (anim["TextSelect"].time / anim["TextSelect"].length > end) 
        {
            SelectEnded(whenSelected);
        }
    }

    private void SelectEnded(Transition t)
    {
        SoundManager.GetSingleton.PlaySound("EnemyDeath");
        switch(t)
        {
            case Transition.PLAY:
                InitLocal.Instance.Play();
                break;
            case Transition.CREDITS:
                break;
            case Transition.QUIT:
                InitLocal.Instance.Quit();
                break;
        }
    }


    private void Select(bool select)
    {
        isSelecting = select;

        if(select)
        {
            anim["TextSelect"].speed = speed;
        }
        else
        {
            anim["TextSelect"].speed = -speed;
        }
    }

}
