using UnityEngine;

public class Swing : MonoBehaviour
{
    [SerializeField] private KeyCode _swingKey = KeyCode.Space;
    [SerializeField] private Rigidbody _swingSeat;
    [SerializeField] private float _swingForce = 10f;
    [SerializeField] private float _drag = 1f; 

    private Vector3 _swingDirection;

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(_swingKey))
        {
            _swingDirection = _swingSeat.velocity.normalized;
            _swingSeat.AddForce(_swingDirection * _swingForce, ForceMode.Impulse);
        }

        _swingSeat.velocity -= _swingSeat.velocity * _drag * Time.deltaTime;
    }
}
