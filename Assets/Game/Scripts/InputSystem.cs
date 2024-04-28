using UnityEngine;
using UnityEngine.Events;

public class InputSystem : MonoBehaviour
{
    private string _horizontalName = "Horizontal";
    private Vector2 direction;

    private KeyCode _jumpKey = KeyCode.Space;
    private KeyCode _shootKey = KeyCode.L;

    public event UnityAction<Vector2> HorisontalKeyGeted;
    public event UnityAction JumpKeyGeted;
    public event UnityAction ShootKeyGeted;


    private void Update()
    {
        GetKey();
    }

    private void GetKey()
    {
        direction.x = Input.GetAxis(_horizontalName);
        HorisontalKeyGeted?.Invoke(direction);

        if (Input.GetKeyDown(_shootKey))
        {
            ShootKeyGeted?.Invoke();
        }

        if (Input.GetKeyDown(_jumpKey))
        {
            JumpKeyGeted?.Invoke();
        }
    }
}
