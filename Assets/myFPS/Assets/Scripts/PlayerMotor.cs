﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private Vector3 cameraRotation = Vector3.zero;


    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Gets a movement vector
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    public void Rotate(Vector3 _rotation)
    {
        if (_rotation != Vector3.zero) // Don't let it reset to neutral too quickly - resets to zero in fixedupdate
        {
            rotation = _rotation;
        }
        

    }

    public void RotateCamera(Vector3 _cameraRotation)
    {
        if (_cameraRotation != Vector3.zero) // Don't let it reset to neutral too quickly - resets to zero in fixedupdate
        {
            cameraRotation = _cameraRotation;
        }
    }

    private void FixedUpdate()
    {
        PerformMovement();
        PerformRotation();
    }

    //Perform movement based on velocity variable
    private void PerformMovement()
    {
        if(velocity != Vector3.zero) 
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime); // Moves to set position including stopping etc.
        }
        
    }

    private void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));

        rotation = Vector3.zero;

        if (cam != null)
        {
            cam.transform.Rotate(cameraRotation);
        }
    }
}
