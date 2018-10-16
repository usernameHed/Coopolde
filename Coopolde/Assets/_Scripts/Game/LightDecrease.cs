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
    private Transform triggerIARed;
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
    [SerializeField, FoldoutGroup("light")]
    private float lightToGameOver = 40f;

    [SerializeField, FoldoutGroup("light"), ReadOnly]
    private float lightIntensity = 100;

    private float maxSizeTrigger;
    private float maxSizeLamp;

    private bool gameIsOver = false;

    private void OnEnable()
    {
        EventManager.StartListening(GameData.Event.GameOver, GameOver);
    }

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
        triggerIARed.localScale = new Vector3(newSizeTrigger, newSizeTrigger, newSizeTrigger);

        float newSizeLight = lightIntensity * maxSizeLamp / lightMax;
        lights[0].spotAngle = newSizeLight;
        lights[1].spotAngle = newSizeLight;
    }

    public void GameOver()
    {
        Debug.Log("game over !!");

        gameIsOver = true;
    }

    private void TryToGameOver()
    {
        if (gameIsOver && lightIntensity < lightToGameOver)
        {
            InitLocal.Instance.Previous();
        }
    }

    private void Update()
    {
        AddOrDecrease();
        TryToGameOver();
    }

    private void OnDisable()
    {
        EventManager.StopListening(GameData.Event.GameOver, GameOver);
    }
}
