using UnityEngine;

[RequireComponent (typeof(Player))]
public class PlayerTaker : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void TakeCoin(Coin coin)
    {
        coin.BecomeRecived();
    }

    private void TakeApple(Apple apple)
    {
        _player.HealthOwn.Heal(apple.HealingPower);
        apple.BecomeRecived();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Coin>(out Coin coin))
        {
            TakeCoin(coin);
        }
        else if (collision.gameObject.TryGetComponent<Apple>(out Apple apple))
        {
            TakeApple(apple);
        }
    }
}
