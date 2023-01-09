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

    const int LIFE_TO_RESTORE = 1;
    const int LIFE_TO_INCREASE = 1;
    const int DAMAGE_TO_TAKE = 1;
    const int MIN_HEALTH = 0;

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

    #endregion

    #region PROPERTIES
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
        if (_health <= MIN_HEALTH)
        {
            return;
        }
        _health -= DAMAGE_TO_TAKE;

        OnDamageTaken?.Invoke();
    }

    public void Heal()
    {
        if (_health >= _maxHealth)
        {
            return;
        }
        _health += LIFE_TO_RESTORE;

        OnRestoreHP?.Invoke();
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
        _maxHealth += LIFE_TO_INCREASE; // "IVAN" Usar ++ o dejarlo para un objeto especial

        if (_healOnMaxHpUp) // "IVAN" Se usa arriba ¿Hacer función?
        {
            Heal();
        }

        OnMaxHealthIncreased?.Invoke();
    }

    #endregion

    #region MONOBEHAVIOUR

    private void Start()
    {
        _health = _maxHealth;
    }

    #endregion
}
