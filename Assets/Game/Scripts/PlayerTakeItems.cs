using UnityEngine;

public class PlayerTakeItems : MonoBehaviour
{
    private PlayerHealth _health;

    private void Awake()
    {
        _health = GetComponent<PlayerHealth>();
    }

    private void TakeCoin(Coin coin)
    {
        coin.BecomeRecived();
    }

    private void TakeApple(Apple apple)
    {
        _health.Heal(apple.HealingPower);
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
