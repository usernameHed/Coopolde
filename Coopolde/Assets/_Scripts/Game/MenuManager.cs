using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

[TypeInfoBox("[ILevelLocal] Manage Setup Scene behaviour")]
public class MenuManager : InitLocal
{
    public override void InitScene()
    {
        base.InitScene();
        Debug.Log("init scene " + SceneManager.GetActiveScene().name);
    }

    
}
