using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region FIELDS

    private static GameManager _instance = null;

    [Header("Player Data")]
    [Tooltip("Player object")]
    [SerializeField] private GameObject _player = null;

    [Tooltip("PlayerHealth script")]
    [SerializeField] private PlayerHealth _playerHealth = null;

    #endregion

    #region PROPERTIES

    /// <summary> Instance of the script for singleton use. </summary>
    public static GameManager instance
    {
        get { return _instance; }
    }

    /// <summary> Actual HP. </summary>
    public PlayerHealth playerHealth
    {
        get { return _playerHealth; }
    }

    #endregion

    #region MONOBEHAVIOUR

    private void Reset()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerHealth = _player.GetComponent<PlayerHealth>();

    }

    private void Awake()
    {
        if (_instance == null) // instance? //??
        {
            _instance = this;
            return;
        }
        Destroy(gameObject); // "IVAN" ¿Es mejor destruir script?
    }

    #endregion
}
