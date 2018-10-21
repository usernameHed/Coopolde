using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[TypeInfoBox("[ILevelLocal] Manage Setup Scene behaviour")]
public class CoopoldeManager : SingletonMono<CoopoldeManager>
{
    public LifeEntity meHP;
    [SerializeField]
    public MySelf mySelf;
    [SerializeField]
    private Me me;
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private int bonus = 0;

    [SerializeField]
    private FrequencyCoolDown addDifficultyOverTime;
    [SerializeField]
    private float difficultyOverTime;

    public float difficulty = 1;

    private void Start()
    {
        addDifficultyOverTime.StartCoolDown();
        bonus = 0;
    }

    public void AddBonus(int add)
    {
        bonus += add;
        text.text = bonus + "";
    }

    private void Update()
    {
        if (addDifficultyOverTime.IsStartedAndOver())
        {
            difficulty+=difficultyOverTime;
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
