using UnityEngine;

public class Coin : MonoBehaviour
{
    public void BecomeRecived()
    {
        Destroy(gameObject);
    }
}
