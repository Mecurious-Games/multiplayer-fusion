using UnityEngine;
using UnityEngine.InputSystem;
using Fusion;

public class FirstPersonCamera : NetworkBehaviour
{
    public Transform Target;
    public float MouseSensitivity = 10f;
    public Vector3 Offset = new Vector3(0f, 0f, -3f);

    private float horizontalRotation;
    private float verticalRotation;

    public override void Spawned()
    {
        if (Target == null)
        {
            Debug.LogError("Target is not assigned to the FirstPersonCamera script.");
            return;
        }

        // Set camera position and rotation based on the target's initial rotation
        // set target as parent
        transform.SetParent(Target);
        transform.rotation = Target.rotation * Quaternion.Euler(0f, 180f, 0f);
        transform.position = Target.position + transform.rotation * Offset;
    }

    private void LateUpdate()
    {
        if (!HasStateAuthority || Target == null)
        {
            return;
        }

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        float mouseX = mouseDelta.x;
        float mouseY = mouseDelta.y;

        horizontalRotation += mouseX * MouseSensitivity;
        verticalRotation -= mouseY * MouseSensitivity;

        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        Quaternion rotation = Quaternion.Euler(verticalRotation, horizontalRotation, 0f);
        Target.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);

        transform.localRotation = Quaternion.identity; // Reset camera rotation
        transform.localPosition = rotation * Offset;
    }
}