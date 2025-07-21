using UnityEngine;
using Zenject;

public class Spawner : MonoBehaviour
{
    private EffectorFactory _effectorFactory;
    private SnakeFactory _snakeFactory;

    private Snake _snake;
    private Door _door;
    private Level _levelConfig;

    [Inject]
    private void Construct(EffectorFactory effectorFactory, SnakeFactory snakeFactory)
    {
        _effectorFactory = effectorFactory;
        _snakeFactory = snakeFactory;
    }

    private void OnDisable()
    {
        _snake.Eating -= CreateBone;
    }

    public void Spawn(GameplayMediator mediator, Level level)
    {
        _levelConfig = level;
        CreateSnake(_levelConfig.SnakeConfig);

        foreach (var config in _levelConfig.EffectorConfigs)
        {
            CreateEffector(config, _snake);
        }

        mediator.Initialize(_snake, _door, level);
    }

    private void CreateEffector(EffectorConfig config, Snake snake)
    {
        Effector effector = _effectorFactory.Get(config, snake);

        if(effector is Door)
        {
            _door = (Door) effector;
            _door.Initialize(((DoorConfig)config).BonesToNextLevel);
        }
    }

    private void CreateSnake(SnakeConfig config)
    {
        _snake = _snakeFactory.GetSnake(config);
        _snake.Eating += CreateBone;
    }

    private void CreateBone()
    {
        Bone bone = _snakeFactory.GetBone();
        bone.transform.position = _snake.transform.position;
        bone.transform.rotation = Quaternion.identity;
    }
}
