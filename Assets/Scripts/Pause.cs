using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Pause : MonoBehaviour
{

    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject inGameUI;

    public Text pauseText;

    private bool _gameStarted;

    public void Start()
    {
        if (GameController.firstPlay)
        {
            Time.timeScale = 0;
            GameController.isPaused = true;
        }
    }

    public void startGame()
    {
        if (_gameStarted) return;

        Time.timeScale = 1;
        GameController.isPaused = false;
        pauseText.text = "Pause";

        //turn UI game elements on.
        startScreen.SetActive(false);
        inGameUI.SetActive(true);
        AudioManager.Instance.PlaySound(AudioManager.Sound.SoundName.Play);

        _gameStarted = true;
    }

    public void pauseGame()
    {
        if (!_gameStarted) return;
        if (GameController.isPaused)
        {
            Time.timeScale = 1;
            GameController.isPaused = false;
            pauseText.text = "Pause";

        }
        else
        {
            Time.timeScale = 0;
            GameController.isPaused = true;
            pauseText.text = "Resume";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
    }
}
