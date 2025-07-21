using UnityEngine;

[CreateAssetMenu(menuName = "Configs/Effectors/TrapConfig", fileName = "TrapConfig")]
public class TrapConfig : EffectorConfig
{
    [SerializeField] private Trap _trap;

    private void OnValidate()
    {
        base.OnValidate();

        if (_trap == null)
        {
            LogErrorAndStopGame();
        }
    }

    public override Effector GetPrefab()
    {
        return _trap;
    }
}
