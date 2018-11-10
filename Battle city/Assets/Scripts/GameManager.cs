using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{        
    public float StartDelay = 3f;         
    public float EndDelay = 3f;              
    public Text MessageText;              
    public GameObject Player;
    public GameObject Enemy;
    public Spawner Spawner;
    public float SpawnDelay = 10;
    public int MaxEnemyCount;
    public GameObject Battlefield;
    public Text LevelText;
    public Text EnemyCountText;
    public Button MenuButton;
    public Text LifeCountText;
    public ScoreCounter ScoreCounter;
    private Stats Base;

    private float LastSpawn = 0;
    private int LevelNumber = 0;
    private int HPMultiplayer = 10;
    private bool IsGameover = false;              
    private WaitForSeconds StartWait;     
    private WaitForSeconds EndWait;
    private int EnemyCounter;
    private Vector3 TempPosition;
    private int Score; 
               
    private void Start()
    {
        StartWait = new WaitForSeconds(StartDelay);
        EndWait = new WaitForSeconds(EndDelay);
        EnemyCounter = 0;
        Enemy.GetComponent<Stats>().Health = 20;
        TankHealth.LifeCount = 2;
        Spawner.Spawn(true, Player);
        TempPosition = MenuButton.transform.position;
        MenuButton.transform.position = Vector3.down * 100;

        StartCoroutine(GameLoop());
    }

    private void Update()
    {
        SpawnEnemies();

        if (TankHealth.Dead)
        {
            ResetPlayer();
        }

        GameOver();
    }

    private void SpawnEnemies()
    {

        if (EnemyCounter < MaxEnemyCount)
        {
            if ((Time.time - LastSpawn) >= SpawnDelay)
            {

                 Spawner.Spawn(false, Enemy);
                EnemyCounter++;

                LastSpawn = Time.time;
            }
            else
                return;
        }
        else
            return;
    }

    
    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        StartCoroutine(GameLoop());
    }

    
    private IEnumerator RoundStarting()
    {
        Instantiate(Battlefield);
        EnemyCounter = 0;
        EnemyHealth.EnemyCount = MaxEnemyCount;
        Enemy.GetComponent<Stats>().Health += (LevelNumber * HPMultiplayer);
        //Player.transform.position = Spawner.playerSpawnPoint.position;
        Player.GetComponent<TankHealth>().CurrentHealth = 50;
        Player.GetComponent<TankHealth>().SetHealthUI();
        Base = GameObject.Find("Base_prefab").GetComponent<Stats>();
        Base.Health = 50;
        LevelNumber++;
        MessageText.text = "LEVEL " + LevelNumber;

        yield return StartWait;
    }


    private IEnumerator RoundPlaying()
    {
        MessageText.text = string.Empty;

        while (!IsLevelClear())
        {
            yield return null;
        }
    }

    private IEnumerator RoundEnding()
    {
        ScoreCounter.GetRoundScore(MaxEnemyCount, LevelNumber);

        yield return EndWait;
    }

    private bool IsLevelClear()
    {
        if (EnemyHealth.EnemyCount <= 0)
        {
            return true;
        }

        return false;
    }

    private void GameOver()
    {
        if ((Base.Health <= 0 || TankHealth.LifeCount == 0) && !IsGameover )
        {
            MenuButton.transform.position = TempPosition;
            Score = ScoreCounter.GetFinaleScore(MaxEnemyCount, LevelNumber);
            MessageText.text = "Game over!\nScore = " + Score;
            ScoreCounter.SaveScore();
            IsGameover = true;
        }
        else
        {
            SetUI();
        }
    }


    private void ResetPlayer()
    {
        Spawner.Spawn(true, Player);
        Player.GetComponent<Stats>().Health = 50;
    }

    private void SetUI()
    {
        LevelText.text = "Уровень: " + LevelNumber;
        EnemyCountText.text = "Врагов: " + EnemyHealth.EnemyCount;
        LifeCountText.text = "Жизней: " + TankHealth.LifeCount;
    }
}