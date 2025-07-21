
public class NoMoveConfig : MovableConfig
{
    private float _rotationY;

    public float RotationY => _rotationY;

    public NoMoveConfig(float rotationY)
    {
        _rotationY = rotationY;
    }
}
