using UnityEngine;

/// <summary>
/// --Player Health--<para></para>
/// 
/// This script controls the health of the player, their actual and max number of hearts
/// Also has the upper limit for the number of hearts
/// It has methods to take damage, heal and increase max hp.
/// </summary>
/// 
public class PlayerHealth : MonoBehaviour
{
    #region FIELDS

    const int LIFETORESTORE = 1;
    const int LIFETOINCREASE = 1;
    const int DAMAGETOTAKE = 1;
    const int MINHEALTH = 0;

    [Header("Health")]
    [Tooltip("Upper limit for health or heart containers.")]
    [SerializeField] private int _maxHPLimit = 10;
    [Tooltip("Maximum health or heart containers.")]
    [SerializeField] private int _maxHealth = 3;
    [Tooltip("Actual health or heart containers.")]
    [SerializeField] private int _health = 1;


    [Header("Heal on MaxHP upgrade")]
    [Tooltip("Heal a heart when MaxHP should increase.")]
    [SerializeField] private bool _healOnMaxHpUp = false;

    private static PlayerHealth _instance = null;

    #endregion

    #region PROPERTIES
    /// <summary> Instance of the script for singleton use. </summary>
    public static PlayerHealth instance
    {
        get { return _instance; }
    }
    /// <summary> Upper HP limit. </summary>
    public int maxHPLimit
    {
        get { return _maxHPLimit; }
        set { _maxHPLimit = value; }
    }
    /// <summary> Maximum HP. </summary>
    public int maxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }
    /// <summary> Actual HP. </summary>
    public int health
    {
        get { return _health; }
        set { _health = value; }
    }
    /// <summary> If player recovers HP when maxHP increases. </summary>
    public bool healOnMaxHpUp
    {
        get { return _healOnMaxHpUp; }
        set { _healOnMaxHpUp = value; }
    }

    /// <summary> Delegate to call when need to update hearts. </summary>
    public delegate void UpdateHearts();
    /// <summary> Delegate to call when need to add hearts. </summary>
    public delegate void AddHearts();

    /// <summary> Event when player damage is taken. </summary>
    public static event UpdateHearts OnDamageTaken;
    /// <summary> Event when player hp is restored. </summary>
    public static event UpdateHearts OnRestoreHP;
    /// <summary> Event when player max hp is increased. </summary>
    public static event AddHearts OnMaxHealthIncreased;

    #endregion

    #region METHODS
    public void TakeDamage()
    {
        if (_health <= MINHEALTH)
        {
            return;
        }
        _health -= DAMAGETOTAKE;

        if (OnDamageTaken != null)
        {
            OnDamageTaken.Invoke();
        }
    }

    public void Heal()
    {
        if (_health >= _maxHealth)
        {
            return;
        }
        _health += LIFETORESTORE;


        if (OnRestoreHP != null)
        {
            OnRestoreHP.Invoke();
        }
    }

    public void UpgradeHealth()
    {
        if (_maxHealth >= _maxHPLimit)
        {
            if (_healOnMaxHpUp)
            {
                Heal();
            }
            return;
        }
        _maxHealth += LIFETOINCREASE; // "IVAN" Usar ++ o dejarlo para un objeto especial

        if (_healOnMaxHpUp) // "IVAN" Se usa arriba ¿Hacer función?
        {
            Heal();
        }

        if (OnMaxHealthIncreased != null)
        {
            OnMaxHealthIncreased.Invoke();
        }
    }

    #endregion

    #region MONOBEHAVIOUR

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            return;
        }
        Destroy(gameObject); // "IVAN" ¿Es mejor destruir script?
    }

    private void Start()
    {
        //_health = _maxHealth;
    }

    #endregion
}
