using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour {

    public bool lightState = false;
    private bool fireA;

    [SerializeField] private GameObject redLight;
    [SerializeField] private GameObject whiteLight;

    [SerializeField] private float rotateSpeed;

    [SerializeField] private float maxAngle;


    [SerializeField] private float selectAngle;
    [SerializeField] private float playPos;
    [SerializeField] private float creditPos;
    [SerializeField] private float quitPos;

    [SerializeField] private float angle;

    // Update is called once per frame
    void Update () 
    {
        float horiz = PlayerConnected.Instance.GetPlayer(0).GetAxis("Move Horizontal");

        if((horiz < 0 && angle < -maxAngle) || (horiz > 0 && angle > maxAngle) )
        {
            //t'es au max bébé ;)
        }
        else
        {
            transform.RotateAround(transform.position, transform.up, horiz * rotateSpeed * Time.deltaTime);

            angle += horiz * rotateSpeed * Time.deltaTime;
        }

        fireA = PlayerConnected.Instance.GetPlayer(0).GetButton("FireA");

        if (fireA != lightState) SwitchLight();
    }

    private void SwitchLight()
    {
        lightState = !lightState;
        redLight.SetActive(!fireA);
        whiteLight.SetActive(fireA);
    }
}
