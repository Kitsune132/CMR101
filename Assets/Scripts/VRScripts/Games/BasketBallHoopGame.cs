using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BasketBallHoopGame : MonoBehaviour
{
    public float threePointerThresholdDistance = 5; // the distance the player is from the hoop when the ball was thrown.
    public Transform playerTransform; // a reference to the players transform.
    public int regularPointValue = 1; // the value of a regular throw.
    public int threePointerValue = 3; // the value of a three pointer throw
    public int currentScore = 0; // the current score
    public int maxScore = 10; // the goal score we are aiming for.
    public Text scoreText; // reference to the score text;

    public UnityEvent onBasketBallGameCompleted; 

    public int CurrentScore
    {
        get
        {
            return currentScore;
        }
        set
        {
            currentScore = value;
            if(currentScore >= maxScore)
            {
                onBasketBallGameCompleted?.Invoke();
            }

            // update our score
            if(scoreText == null)
            {
                Debug.LogError("No reference assigned in the inspector for UI score");
                return;
            }
            else
            {
                scoreText.text = currentScore + "/" + maxScore;
            }
        }
    }

    /// <summary>
    /// This is called when the basketball has been thrown into the hoop.
    /// </summary>
    public void OnBasketScored()
    {
        // if we get to in here, then essentially the ball has landed in the hoop
        // if that is the case then lets check the distance the player was at at the time of throwing it.
        
        if(Vector3.Distance(transform.position, playerTransform.position) >= threePointerThresholdDistance)
        {
            CurrentScore += threePointerValue;
        }
        else
        {
            CurrentScore += regularPointValue;
        }
    }

    public void ResetGame()
    {
        CurrentScore = 0;
    }
}


