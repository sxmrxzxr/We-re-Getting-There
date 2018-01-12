using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameResult
{
    InProgress,
    Win,
    Loss
}

public class GameManager : MonoBehaviour {

    public Transform pauseCanvas;

    public SimpleBusController bus;
    public RouteController route;

    public float timeLimit = 60;
    public float timerValue = -1;
    public GameResult gameResult;

    private IEnumerator StartTimer()
    {
        while (timerValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            timerValue--; 
        }
    }

    public string GetCurrentTimerValue()
    {
        return TimeSpan.FromSeconds(Convert.ToDouble(timerValue)).ToString();
    }

    public string GetGameResult()
    {
        switch (gameResult) {
            case GameResult.InProgress:
                return "";
            case GameResult.Win:
                return "You Win!";
            case GameResult.Loss:
                return "You Lose!";
            default:
                return "";
        }
    }

	// Use this for initialization
	void Start () {
        bus.transform.position = route.StartNode.transform.position;
        gameResult = GameResult.InProgress;
        timerValue = timeLimit;
        StartCoroutine(StartTimer());
	}

    public void Pause()
    {
        if (!pauseCanvas.gameObject.activeInHierarchy)
        {
            pauseCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseCanvas.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void QuitOnClick()
    {
        SceneManager.LoadScene(0);
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }

		if (timerValue == 0 && !route.IsRouteComplete())
        {
            gameResult = GameResult.Loss;
        } 
        else if (timerValue > 0 && route.IsRouteComplete())
        {
            gameResult = GameResult.Win;
        }
        else
        {

        }
	}
}
