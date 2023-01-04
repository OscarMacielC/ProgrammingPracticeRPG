using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Vector3 _moveDirection;
    private float _speed = 40;

    private void HandleInput()
    {
        //Get the imput for the movement
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        _moveDirection = new Vector3(x, 0.0f, z)*Time.deltaTime*_speed;
        Debug.Log(Input.GetAxisRaw("Horizontal"));
    }

    private void Move()
    {
        //Apply the movement
        transform.Translate(_moveDirection);
    }


    private void FixedUpdate()
    {
        Move();
    }

    private void Update()
    {
        HandleInput();
    }
}

