using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDecrease : MonoBehaviour
{
    [SerializeField]
    private MySelf mySelf;

    [SerializeField]
    private Transform triggerIA;
    [SerializeField]
    private Light[] lights;

    [SerializeField, FoldoutGroup("light")]
    private float lightMax = 100;
    [SerializeField, FoldoutGroup("light")]
    private float lightMin = 10;
    [SerializeField, FoldoutGroup("light")]
    private float speedDecrease = 1f;
    [SerializeField, FoldoutGroup("light")]
    private float speedIncrease = 1f;

    [SerializeField, FoldoutGroup("light"), ReadOnly]
    private float lightIntensity = 100;

    private float maxSizeTrigger;
    private float maxSizeLamp;

    private void Start()
    {
        lightIntensity = lightMax;
        maxSizeTrigger = triggerIA.localScale.x;
        maxSizeLamp = lights[0].spotAngle;
    }

    private void AddOrDecrease()
    {
        if (mySelf.lampActived)
        {
            lightIntensity -= speedDecrease * Time.deltaTime;
        }
        else
        {
            lightIntensity += speedIncrease * Time.deltaTime;
        }
        lightIntensity = Mathf.Clamp(lightIntensity, lightMin, lightMax);

        float newSizeTrigger = lightIntensity * maxSizeTrigger / lightMax;
        triggerIA.localScale = new Vector3(newSizeTrigger, newSizeTrigger, newSizeTrigger);

        float newSizeLight = lightIntensity * maxSizeLamp / lightMax;
        lights[0].spotAngle = newSizeLight;
        lights[1].spotAngle = newSizeLight;
    }

    private void Update()
    {
        AddOrDecrease();
    }
}
