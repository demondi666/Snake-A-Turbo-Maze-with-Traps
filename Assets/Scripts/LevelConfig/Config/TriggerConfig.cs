using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Effectors/TriggerConfig", fileName = "TriggerConfig")]
public class TriggerConfig : EffectorConfig
{
    [SerializeField] private Trigger _trigger;

    private void OnValidate()
    {
        base.OnValidate();

        if (_trigger == null)
        {
            LogErrorAndStopGame();
        }
    }

    public override Effector GetPrefab()
    {
        return _trigger;
    }
}
