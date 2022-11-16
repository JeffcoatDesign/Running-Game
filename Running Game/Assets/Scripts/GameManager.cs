using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public float speedConst;
    public float startSpeed;
    public float currentSpeed;
    public float startTime;
    private float timeTaken;
    public bool gameRunning = false;

    public UnityEvent startGame;
    public UnityEvent loseGame;

    public GameObject playButton;
    public TextMeshProUGUI timerText;

    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void StartGame ()
    {
        gameRunning = true;
        currentSpeed = startSpeed;
        startTime = Time.time;
        playButton.SetActive(false);

        if (startGame != null)
            startGame.Invoke();
    }

    public void Update()
    {
        if (!gameRunning)
            return;

        currentSpeed += speedConst * Time.deltaTime;
        timerText.text = (Time.time - startTime).ToString("F2");
    }

    public void Lose ()
    {
        timeTaken = Time.time - startTime;
        gameRunning = false;
        Leaderboard.instance.SetLeaderboardEntry(Mathf.RoundToInt(timeTaken * 1000.0f));
        currentSpeed = 0;
        playButton.SetActive(true);

        if (loseGame != null)
            loseGame.Invoke();
    }
}
