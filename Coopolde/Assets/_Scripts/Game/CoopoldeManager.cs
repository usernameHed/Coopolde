using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

[TypeInfoBox("[ILevelLocal] Manage Setup Scene behaviour")]
public class CoopoldeManager : SingletonMono<CoopoldeManager>
{
    [SerializeField]
    private MySelf mySelf;
    [SerializeField]
    private Me me;

    public int difficulty = 1;

    public GameObject GetTarget()
    {
        if (!me)
            return (null);
        return (me.rb.gameObject);
    }
}
