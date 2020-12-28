using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [HideInInspector] public UnityEvent<int> scoreIncreaseEvent;
    public static GameController Instance { get; private set; }
    public static bool firstPlay = true;
    public static bool isPaused = false;


    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private int playerStartingLives;
    [SerializeField] private float deathDelayTime;

    [SerializeField] private Text finalScoreText;
    [SerializeField] private Text finalHighScoreText;

    [SerializeField] private GameObject restartButton;
    private Vector2 _playerLocation;

    private int _score;
    private float _gameOverTimer = 0;
    private int _highScore;
    private int _playerLives;


    private Pause _pause;

    // Start is called before the first frame update
    void Awake()
    {
        _pause = GetComponent<Pause>();
        if (!firstPlay)
        {
            _pause.startGame();
        }
        Instance = this;


        _highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = _highScore.ToString();

        _playerLives = playerStartingLives;
    }

    private void Update()
    {
        if (_gameOverTimer > 0)
        {
            _gameOverTimer -= Time.deltaTime;
            if (_gameOverTimer <= 0)
            {
                GameOver();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (_playerLives > 0) _pause.startGame();
            else restartGame();
        }
    }

    private void GameOver()
    {

        firstPlay = false;
        //show player score and top score
        var oldHighScore = PlayerPrefs.GetInt("HighScore", 0);
        PlayerPrefs.SetInt("HighScore", Math.Max(_highScore, oldHighScore));

        finalScoreText.text = _score.ToString();
        finalScoreText.gameObject.SetActive(true);
        finalHighScoreText.text = _highScore.ToString();
        finalHighScoreText.gameObject.SetActive(true);

        //show restart button
        restartButton.SetActive(true);
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        _gameOverTimer = 0f;

        finalHighScoreText.gameObject.SetActive(false);
        finalScoreText.gameObject.SetActive(false);

    }


    public void PlayerHit()
    {
        _playerLives--;
        if (_playerLives <= 0)
        {
            StartGameOver();
        }
    }

    private void StartGameOver()
    {
        _gameOverTimer = deathDelayTime;
    }


    public void AddScore(int amount)
    {
        Debug.Log("Score increased");
        _score += amount;
        scoreText.text = _score.ToString();
        AudioManager.Instance.PlaySound(AudioManager.Sound.SoundName.ScoreIncreased);

        if (_score > _highScore)
        {
            _highScore = _score;
            highScoreText.text = _highScore.ToString();
        }
    }
}
