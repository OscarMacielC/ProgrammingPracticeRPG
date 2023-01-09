using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/// <summary>
/// --Health UI Manager--<para></para>
/// 
/// This script controls the UI for the health of the player
/// Also has the upper limit for the number of hearts
/// Its subcribed to the player events and updates the UI accordingly
/// </summary>
/// <remarks> Execution order should be after PlayerHealth. </remarks>
/// 
public class PlayerHealthUIManager : MonoBehaviour
{
    #region FIELDS

    [Header("Player Data")]
    [Tooltip("Singleton PlayerHealth script")]
    [SerializeField] private PlayerHealth _playerHealth;

    [Header("Hearts")]
    [Tooltip("Gameobject prefab with the image of the full heart")]
    [SerializeField] private GameObject _heart;
    [SerializeField] private Image _heartImage;
    [Tooltip("Gameobject prefab with the image of the hollow heart")]
    [SerializeField] private GameObject _hollowHeart;
    [SerializeField] private Image _hollowHeartImage;

    private struct HeartUI // "Ivan" Como se nombran los structs
    {
        public GameObject HeartGO;
        public Image HeartIMG;
        public Animator HeartAnim;
    }

    private class HeartDictionary : Dictionary<int, HeartUI> // "Ivan" Como se nombran los diccionaries
    {
        private HeartUI newHeart;
        internal void Add(int key, GameObject heartGO)
        {
            newHeart.HeartGO = heartGO;
            newHeart.HeartIMG = heartGO.GetComponent<Image>();
            newHeart.HeartAnim = heartGO.GetComponent<Animator>();
            this.Add(key, newHeart);
        }
    }

    HeartDictionary _heartDictionary = new HeartDictionary();

    #endregion

    #region METHODS

    void TakeDamage() // "Ivan" En lugar de hacer todo esto es mejor un update heartsUI que revise y los haga todos???
    {
        _heartDictionary[_playerHealth.health].HeartAnim.enabled = false;
        _heartDictionary[_playerHealth.health].HeartIMG.sprite = _hollowHeartImage.sprite;
    }

    void HealHearts()
    {
        int heartToChange = _playerHealth.health - 1;
        _heartDictionary[heartToChange].HeartAnim.enabled = true;
    }

    void AddHearts()
    {
        int heartToChange = _playerHealth.maxHealth - 1; // "Ivan" Usar constante del player?? crear nueva o hacerla variable para no usar -1 ni --

        _heartDictionary[heartToChange].HeartGO.SetActive(true);
        _heartDictionary[heartToChange].HeartAnim.enabled = false;
        _heartDictionary[heartToChange].HeartIMG.sprite = _hollowHeartImage.sprite;

        if (_playerHealth.maxHealth >= _playerHealth.maxHPLimit)
        {
            if (_playerHealth.healOnMaxHpUp)
            {
                HealHearts();
            }
            return;
        }

        if (_playerHealth.healOnMaxHpUp) // "IVAN" Se usa arriba ¿Hacer función?
        {
            HealHearts();
        }
    }

    #endregion

    #region MONOBEHAVIOUR

    private void OnEnable()
    {
        PlayerHealth.OnDamageTaken += TakeDamage;
        PlayerHealth.OnRestoreHP += HealHearts;
        PlayerHealth.OnMaxHealthIncreased += AddHearts;
    }

    private void OnDisable()
    {
        PlayerHealth.OnDamageTaken -= TakeDamage;
        PlayerHealth.OnRestoreHP -= HealHearts;
        PlayerHealth.OnMaxHealthIncreased -= AddHearts;
    }

    private void Reset()
    {
        _playerHealth = GameManager.instance.playerHealth;
    }

    void Start()
    {
        _heartImage = _heart.GetComponent<Image>();
        _hollowHeartImage = _hollowHeart.GetComponent<Image>();

        for (int i = default; i < _playerHealth.maxHPLimit; i++)
        {
            GameObject heart = Instantiate(_heart, this.transform);
            Debug.Log("Instanciated" + heart.name);
            _heartDictionary.Add(i, heart);
            _heartDictionary[i].HeartGO.SetActive(false);
            _heartDictionary[i].HeartAnim.enabled = false;
        }
        for (int i = default; i < _playerHealth.maxHealth; i++)
        {
            _heartDictionary[i].HeartGO.SetActive(true);
        }
        for (int i = default; i < _playerHealth.health; i++)
        {
            _heartDictionary[i].HeartAnim.enabled = true;
        }
    }

    #endregion
}
