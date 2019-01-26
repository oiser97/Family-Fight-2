
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager :
    MonoBehaviour
{

    public static GameManager   instance;

    public Player               playerOne,

                                playerTwo;

    public GameObject[]         playerOneLives,

                                playerTwoLives;

    private Timer               gameTimer;

    public Text                 timeText,

                                scorePlayerOneText,

                                scorePlayerTwoText;

    public GameObject           spawnPlayerOne,

                                spawnPlayerTwo;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        gameTimer = new Timer(false, 5f * 60f);
    }

    private void Update()
    {
       if ( Input.GetKeyDown(KeyCode.Q))
            UpdateUI();

        gameTimer.Update();
        GameOverTimer();
        UIScore();
        UILife();
        CheckPlayerLife();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < playerOneLives.Length; i++)
        {
            playerOneLives[i].SetActive(i < playerOne.health);
        }

        for (int i = 0; i < playerTwoLives.Length; i++)
        {
            playerTwoLives[i].SetActive(i < playerTwo.health);
        }

    }

    public void GameOverTimer()
    {
        float remaining_t = gameTimer.RemainingTime() / 60f;
        int dec = (int)((remaining_t - (int)remaining_t) * 100);
        // dec : 100 = x : 60
        // dec * 60 = 100 * x
        // x = dec * 60 / 100
        timeText.text = "TIME REMANING: \n" + ((int)remaining_t) + ":" + (int)(dec * 0.6);
    }

    private void UIScore()
    {
        scorePlayerOneText.text = "SCORE \n";
        scorePlayerTwoText.text = "SCORE \n";

        for (int i = 100; i > 0; i /= 10 )
        {
            if (i > playerOne.score)
            {
                scorePlayerOneText.text += "0";

            }
            else
            {
                scorePlayerOneText.text += playerOne.score;
                break;
            }
        }

        for (int i = 100; i > 0; i /= 10)
        {
            if (i > playerTwo.score)
            {
                scorePlayerTwoText.text += "0";

            }
            else
            {
                scorePlayerTwoText.text += playerTwo.score;
                break;
            }
        }
    }

    public void UILife()
    {
        for(int i = 0; i < playerOneLives.Length; i ++)
        {
            playerOneLives[i].SetActive(i < playerOne.health);

        }

        for (int i = 0; i < playerTwoLives.Length; i++)
        {
            playerTwoLives[i].SetActive(i < playerTwo.health);

        }
    }

    public void CheckPlayerLife()
    {
        if(playerOne.health <= 0)
        {
            playerOne.transform. position = spawnPlayerOne.transform.position;
        }

        if (playerTwo.health <= 0)
        {
            playerTwo.transform.position = spawnPlayerTwo.transform.position;
        }

    }
}
