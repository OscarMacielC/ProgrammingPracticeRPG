using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /// <summary>
    /// Player Movement.
    /// 
    /// 'PlayerMovement' is in charge of moving the character and perform the dashes if there is one available
    /// It does not allow the character to jump or fall, almost like in a 2D game
    /// Rotation will be tracked in a child
    /// </summary>

    #region EDITOR EXPOSED FIELDS

    [Header("Movement")]
    [Tooltip("Maximum movement speed (in m/s), can go negative.")]
    [SerializeField] private float _speed = 40f;

    #endregion

    #region FIELDS

    private Vector3 _moveDirection = default;
    private bool _dashRequest = false;

    #endregion

    #region PROPERTIES

    /// <summary>
    /// Maximum movement speed (in m/s).
    /// </summary>
    public float speed
    {
        get { return _speed; }
        set { _speed = value; }
    }


    #endregion

    #region METHODS

    private void HandleInput()
    {
        //Get the imput for the movement
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        _moveDirection = new Vector3(x, 0.0f, z);

        if (Input.GetButtonDown("Dash"))
        {
            _dashRequest = true;
        }

    }

    private void Move()
    {
        //Apply the movement
        transform.Translate(_moveDirection * Time.deltaTime * _speed);
    }

    private void Dash()
    {
        if (_dashRequest)
        {
            transform.Translate(_moveDirection * Time.deltaTime * _speed *30);
            _dashRequest = false;
        }
    }

    #endregion

    #region MONOBEHAVIOUR

    private void Reset()
    {
        _speed = 0f;
    }

    private void FixedUpdate()
    {
        Move();
        Dash();
    }

    private void Update()
    {
        HandleInput();
    }

    #endregion




}

