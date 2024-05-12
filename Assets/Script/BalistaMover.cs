using UnityEngine;

public class BalistaMover : MonoBehaviour
{
    private const string MouseXAxisName = "Mouse X";
    private const string MouseYAxisName = "Mouse Y";
    private const float MinVerticalRotation = 0f;
    private const float MaxVerticalRotation = 15f;
    private const int RotateMouseButton = 2;
    private const float _rotationSpeed = 2f;

    private void Update()
    {
        HandleRotation();
    }

    private void HandleRotation()
    {
        if (Input.GetMouseButton(RotateMouseButton))
        {
            float mouseX = Input.GetAxis(MouseXAxisName);
            float mouseY = Input.GetAxis(MouseYAxisName);
            Vector3 currentRotation = transform.rotation.eulerAngles;
            float rotationY = currentRotation.y + mouseX * _rotationSpeed;
            Quaternion horizontalRotation = Quaternion.Euler(0f, rotationY, 0f);
            float rotationX = currentRotation.x - mouseY * _rotationSpeed;
            rotationX = Mathf.Clamp(rotationX, MinVerticalRotation, MaxVerticalRotation);
            Quaternion verticalRotation = Quaternion.Euler(rotationX, 0f, 0f);
            transform.rotation = horizontalRotation * verticalRotation;
        }
    }
}
