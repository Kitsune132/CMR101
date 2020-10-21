using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Simple bowling lane logic, is triggered externally by buttons that are routed
/// to the InitialiseRound, TalleyScore and ResetRack.
/// 
/// Future work;
///   Use the timer in update to limit how long a player has to bowl,
///   Detect that the player/ball is 'bowled' from behind the line
/// </summary>
public class BowlingLaneBehaviour : MonoBehaviour
{
    public GameObject pinPrefab;
    public GameObject bowlingBall;
    public Transform[] pinSpawnLocations;
    public Transform defaultBallLocation;
    //TODO; we need a way of tracking the pins that are used for scoring and so we can clean them up
    public List<GameObject> allPinsSpawnedIn = new List<GameObject>();

    public float bowlingPinKnockedThreshold = 0.1f; // the distance from spawn location of the pin to the current distance of each pin

    public int currentScore = 0;
    public int maxScore = 10;

    public Text bowlingLaneText;
    public UnityEvent onBowlingGameCompleted;


    private bool EditModeActive = false;
    private void OnEnable()
    {
        GameEvents.OnEditActive += () => { EditModeActive = true; };
        GameEvents.OnEditDeActive += () => { EditModeActive = false; };
    }

    private void OnDisable()
    {
        GameEvents.OnEditActive -= () => { EditModeActive = true; };
        GameEvents.OnEditDeActive -= () => { EditModeActive = false; };
    }


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
                bowlingLaneText.text = "Strike!";
                onBowlingGameCompleted?.Invoke();
            }
            else
            {
                bowlingLaneText.text = currentScore + "/" + maxScore;
            }
        }
    }

    [ContextMenu("InitialiseRound")]
    public void InitialiseRound()
    {
        if(EditModeActive)
        {
            return;
        }

        ResetRack();     
        
    }

    public void BallReachedEnd()
    {
        if (EditModeActive)
        {
            return;
        }
        //TODO; this needs to return the ball to the ball feed so the player could bowl again or at least clean ups
        bowlingBall.transform.position = defaultBallLocation.position;
        bowlingBall.GetComponent<Rigidbody>().velocity = Vector3.zero;
        bowlingBall.GetComponent<Rigidbody>().angularVelocity = Vector3.zero; 
    }

    [ContextMenu("TalleyScore")]
    public void TalleyScore()
    {
        if (EditModeActive)
        {
            return;
        }
        //TODO; determine score and get that information out to a checklist item, either via event or directly

        if (allPinsSpawnedIn.Count != pinSpawnLocations.Length)
        {
            Debug.LogError("Something has gone seriously wrong fix it");
        }

        for (int i = 0; i < allPinsSpawnedIn.Count ; i++)
        {
            if(Vector3.Distance(allPinsSpawnedIn[i].transform.position,pinSpawnLocations[i].position) >= bowlingPinKnockedThreshold)
            {
                CurrentScore++;
            }
        }
    }

    [ContextMenu("ResetRack")]
    public void ResetRack()
    {
        if (EditModeActive)
        {
            return;
        }
        //TODO; clean up all objects created by the bowling lane, preparing for a new round of bowling to occur
        for (int i = 0; i < allPinsSpawnedIn.Count; i++)
        {
            Destroy(allPinsSpawnedIn[i]);
        }
        allPinsSpawnedIn.Clear();
        for (int i = 0; i < pinSpawnLocations.Length; i++)
        {
            GameObject clone = Instantiate(pinPrefab, pinSpawnLocations[i].position, pinSpawnLocations[i].transform.rotation);
            allPinsSpawnedIn.Add(clone);
        }

        CurrentScore = 0;
        BallReachedEnd();
    }

    protected void Update()
    {
        
    }
}
