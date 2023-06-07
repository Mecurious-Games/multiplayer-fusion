using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    public Transform Target;
    public float MouseSensitivity = 10f;
    public float RotationSpeed = 5f;
    public Vector3 Offset = new Vector3(0f, 1f, -3f);

    private float horizontalRotation;

    private void LateUpdate()
    {
        if (Target == null)
        {
            return;
        }

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x;
        float mouseY = mouseDelta.y;

        horizontalRotation += mouseX * MouseSensitivity;

        Quaternion rotation = Quaternion.Euler(0, horizontalRotation, 0);
        transform.rotation = rotation;

        Vector3 desiredPosition = Target.position + Offset;
        transform.position = desiredPosition;

        Target.Rotate(Vector3.up, mouseX * RotationSpeed);
    }
}