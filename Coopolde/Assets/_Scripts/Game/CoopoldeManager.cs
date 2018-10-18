using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

[TypeInfoBox("[ILevelLocal] Manage Setup Scene behaviour")]
public class CoopoldeManager : SingletonMono<CoopoldeManager>
{
    [SerializeField]
    public MySelf mySelf;
    [SerializeField]
    private Me me;

    [SerializeField]
    private FrequencyCoolDown addDifficultyOverTime;

    public int difficulty = 1;

    private void Start()
    {
        addDifficultyOverTime.StartCoolDown();
    }

    private void Update()
    {
        if (addDifficultyOverTime.IsStartedAndOver())
        {
            difficulty++;
            addDifficultyOverTime.StartCoolDown();
        }
    }

    public GameObject GetTarget()
    {
        if (!me)
            return (null);
        return (me.rb.gameObject);
    }
}
