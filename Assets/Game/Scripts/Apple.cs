using UnityEngine;

public class Apple : MonoBehaviour
{
    private int _healingPower;

    public int HealingPower => _healingPower;

    private void Awake()
    {
        _healingPower = 40;
    }

    public void BecomeRecived()
    {
        Destroy(gameObject);
    }
}
