using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using System;
using Sirenix.Serialization;
using UnityEngine.SceneManagement;

/// <summary>
/// GameManager
/// </summary>
[TypeInfoBox("Global GameManager (entire project)")]
[TypeInfoBox("Has link to Camera, SceneManager")]
public class GameManager : SingletonMono<GameManager>
{
    [HideInInspector]
    public bool newScene = false;

    private GameObject cameraObject;
    public GameObject GetCamera()
    {
        if (!cameraObject)
        {
            cameraMain = Camera.main;
            cameraObject = cameraMain.gameObject;
            return (cameraObject);
        }
        return cameraObject;
    }
    public void SetCamera(GameObject cam) { cameraMain = (cam) ? cam.GetComponent<Camera>() : null; }
    private Camera cameraMain;
    public Camera CameraMain { get { return (cameraMain); } }

    private void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log("Init main GameManager of the game once");
        Application.targetFrameRate = 60;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //Debug.Log("OnSceneLoaded: " + scene.name);
        InitLocal initLocal = InitLocal.Instance;
        if (initLocal)
            initLocal.InitScene();
        //Debug.Log(mode);
    }

    /// <summary>
    /// initialise les ILevelManagers (il y en a forcément 1 par niveau)
    /// </summary>
    public void InitNewScene()
    {
        //init la caméra de la scene...
        cameraObject = GetCamera();
        SetCamera(cameraObject);

        //init le level...
        newScene = false;
        //SceneManagerGlobal.Instance.ResetRetry();
        InitLocal.Instance.InitScene();
        //sceneManagerLocal.LevelManagerScript.InitScene();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
