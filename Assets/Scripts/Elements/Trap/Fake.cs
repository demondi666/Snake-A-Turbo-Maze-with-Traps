public class Fake : Booster
{
    protected override void EventHappened(Snake snake)
    {
        snake.Die();
        gameObject.SetActive(false);
    }
}
