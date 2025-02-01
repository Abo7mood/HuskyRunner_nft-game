using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    #region Constructers
    public static GameManager instance;

    TimeManager timer;
    PlayerController player;
    PlayFabManager playfab;
    [Header("StatsPanel")]
    [SerializeField] TextMeshProUGUI coins;
    [SerializeField] TextMeshProUGUI score;
    [SerializeField] TextMeshProUGUI scoreDieTxt;

    #endregion
    #region int&float
    [HideInInspector] public int coinsAmount;
    [HideInInspector] public int scoreAmount;
    [HideInInspector] public int scoreAmountDie;
    float timescaling;
    [SerializeField] int scoreProgress;
    [Header("EnemiesBonus")]
    public int greenEnemyBonus;
    public int birdEnemyBonus;
    #endregion


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            if (instance != this)
            {
                Destroy(instance.gameObject);
                instance = this;

            }
           
        }
        timer = FindObjectOfType<TimeManager>().GetComponent<TimeManager>();
        player = FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        playfab = FindObjectOfType<PlayFabManager>().GetComponent<PlayFabManager>();
    }
    void Start()
    {
        coinsAmount = 0;
        scoreAmount = 0;
    }
    void Update()
    {
        ChangeValues();
        UpdateValue();
        timescaling = Time.timeScale;
    }

    void UpdateValue()
    {
        coins.text = coinsAmount.ToString();
        score.text = scoreAmount.ToString();
        scoreDieTxt.text = scoreAmount.ToString();
    }
    void ChangeValues() => scoreAmount += ((int)player.speed * scoreProgress) * (int)timescaling / 1000;
    public void LoadScene(int index) => SceneManager.LoadScene(index);
    #region Playfab
    public void Getleaderboard() => playfab.GetLeaderboard();
    #endregion
}

