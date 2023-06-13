using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GoalGameManager : MonoBehaviour
{
    [Header("Goal area animator, used to setup and move the area")]
    [SerializeField]
    private Animator goalGameAnimator;
    [Header("Reference to the ball prefab to be used in game")]
    [SerializeField]
    private GameObject ballPrefab;
    

    [Header("Goal panel, materials and canvas references")]
    [SerializeField]
    private GameObject goalPanel;
    [SerializeField]
    private Material goalHighlightedMaterial;
    [SerializeField]
    private Material goalIdleMaterial;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private GameObject goalScoredCanvas;

    private GameObject currentBall;
    private int score = 0;
    private bool gameStarted = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            StartGame();
        }
    }

    public void StartGame()
    {
        score = 0;
        scoreText.text = score.ToString();
        goalGameAnimator.SetTrigger(Constant.PLAY_GAME);
        CreateBall(null, null);
        gameStarted = true;
    }

    public void StopGame()
    {
        goalGameAnimator.SetTrigger(Constant.STOP_GAME);
        Destroy(currentBall);
        gameStarted = false;
    }

    private void CreateBall(object sender, EventArgs e)
    {
        StartCoroutine(DoCreateBall(sender));        
    }

    private IEnumerator DoCreateBall(object sender)
    {
        yield return new WaitForSeconds(Constant.CREATING_BALL_TIME);
        if (sender != null)
        {
            Destroy((GameObject)sender);
        }
        if (!gameStarted)
        {
            //this avoids case where the game is stopped after a shoot; in that case the event could be already invoked
            yield break;
        }
        currentBall = Instantiate(ballPrefab, Camera.main.transform);
        BallController ballController = currentBall.GetComponent<BallController>();
        ballController.onBallShooted += CreateBall;
        ballController.onGoalShooted += NewGoal;
    }

    private void NewGoal(object sender, EventArgs e)
    {
        score++;
        PrintScore();
        StartCoroutine(HighlightGoalPanel());
    }

    IEnumerator HighlightGoalPanel()
    {
        goalScoredCanvas.SetActive(true);
        goalPanel.GetComponent<Renderer>().sharedMaterial = goalHighlightedMaterial;       
        yield return new WaitForSeconds(Constant.HIGHLIGHT_GOAL_TIME);
        goalScoredCanvas.SetActive(false);
        goalPanel.GetComponent<Renderer>().sharedMaterial = goalIdleMaterial;
    }

    private void PrintScore()
    {
        scoreText.text = score.ToString();
    }
}
