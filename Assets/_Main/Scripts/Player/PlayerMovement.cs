using UnityEngine;

/// <summary>
/// --Player Movement--<para></para>
/// 
/// Is in charge of moving the character and perform the dashes if there is one available
/// It does not allow the character to jump or fall, almost like in a 2D game
/// Rotation will be tracked in a child
/// </summary>
/// 
public class PlayerMovement : MonoBehaviour
{
    #region FIELDS

    [Header("Movement")]
    [Tooltip("Maximum movement speed (in m/s), can go negative.")]
    [SerializeField] private float _speed = 40f;

    const string HORIZONTALAXISSTR = "Horizontal";
    const string VERTICALAXISSTR = "Vertical";
    const string DASHSTR = "Dash";


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
        float x = Input.GetAxisRaw(HORIZONTALAXISSTR);
        float z = Input.GetAxisRaw(VERTICALAXISSTR);
        _moveDirection = new Vector3(x, default, z);

        if (Input.GetButtonDown(DASHSTR))
        {
            _dashRequest = true;
        }
    }

    private void Move()
    {
        transform.Translate(_moveDirection * Time.deltaTime * _speed);
    }

    private void Dash()
    {
        if (_dashRequest)
        {
            transform.Translate(_moveDirection * Time.deltaTime * _speed * 30);
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
