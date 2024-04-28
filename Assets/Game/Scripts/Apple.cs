using UnityEngine;

public class Apple : MonoBehaviour
{
    private int _healingPower = 40;

    public int HealingPower => _healingPower;

    public void BecomeRecived()
    {
        Destroy(gameObject);
    }
}
