using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] // Make this show in the inspector even if private. Allows easy edit of protected values
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 10f;

    private PlayerMotor motor;

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
        // Calculate movement velocity as Vector3D
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov; // Use Transform.right rather than Vector.right to keep rotation 
        Vector3 _movVertical = transform.forward * _zMov;

        // Final movement vector
        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;

        // Apply movement
        motor.Move(_velocity);

        // Calculate rotation as 3D vector for turning
        float _yRot = Input.GetAxisRaw("Mouse X");
        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity; // Turning only, not vertical look (which is camera-only)

        // Apply rotation
        motor.Rotate(_rotation);

        // Calculate camera rotation as 3D vector (Vertical axis)
        float _xRot = Input.GetAxisRaw("Mouse Y");
        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity; // Turning only, not vertical look (which is camera-only)

        // Apply camera-only rotation
        motor.RotateCamera(_cameraRotation);
    }


}
