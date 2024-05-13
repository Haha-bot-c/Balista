using UnityEngine;

public class CameraMover : MonoBehaviour
{
    private const string ZoomAxisName = "Mouse ScrollWheel";
    private const float MinZoom = 5f;
    private const float MaxZoom = 50f;
    private const string MouseXAxisName = "Mouse X";
    private const string MouseYAxisName = "Mouse Y";
    private const int MiddleMouseButton = 1;

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private Terrain _terrain;
    [SerializeField] private float _zoomSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 2f;

    private KeyCode _forwardKey = KeyCode.W;
    private KeyCode _backwardKey = KeyCode.S;
    private KeyCode _leftKey = KeyCode.A;
    private KeyCode _rightKey = KeyCode.D;

    private void Update()
    {
        Vector3 currentPosition = transform.position;

        HandleMovement(ref currentPosition);
        ClampCameraPosition(ref currentPosition);
        HandleZoom();
        HandleRotation();

        transform.position = currentPosition;
    }

    private void HandleMovement(ref Vector3 position)
    {
        Vector3 forwardDirection = transform.forward;
        Vector3 rightDirection = transform.right;

        forwardDirection.y = 0f;
        rightDirection.y = 0f;
        forwardDirection.Normalize();
        rightDirection.Normalize();

        if (Input.GetKey(_forwardKey))
        {
            position += forwardDirection * _moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(_backwardKey))
        {
            position -= forwardDirection * _moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(_leftKey))
        {
            position -= rightDirection * _moveSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(_rightKey))
        {
            position += rightDirection * _moveSpeed * Time.deltaTime;
        }
    }

    private void ClampCameraPosition(ref Vector3 position)
    {
        position = new Vector3(
            Mathf.Clamp(position.x, 0, _terrain.terrainData.size.x),
            position.y,
            Mathf.Clamp(position.z, 0, _terrain.terrainData.size.z)
        );
    }

    private void HandleZoom()
    {
        float zoomAmount = Input.GetAxis(ZoomAxisName) * _zoomSpeed;
        Camera.main.fieldOfView -= zoomAmount;
        Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, MinZoom, MaxZoom);
    }

    private void HandleRotation()
    {
        if (Input.GetMouseButton(MiddleMouseButton))
        {
            float mouseX = Input.GetAxis(MouseXAxisName);
            float mouseY = Input.GetAxis(MouseYAxisName);

            transform.Rotate(Vector3.up, mouseX * _rotationSpeed, Space.World);
            transform.Rotate(Vector3.left, mouseY * _rotationSpeed, Space.Self);
        }
    }
}
