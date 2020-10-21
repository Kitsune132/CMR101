using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ZombieManager : MonoBehaviour
{
    public List<Zombie> allZombiesInScene = new List<Zombie>();
    public List<Zombie> activeZombiesInScene = new List<Zombie>();
    public UnityEvent OnZombieGameComplete = new UnityEvent();
    public Text scoreText;
    
    

    public void OnZombieKilled(Zombie zombieToRemove)
    {
        activeZombiesInScene.Remove(zombieToRemove);
        if(allZombiesInScene.Count <=0)
        {
            OnZombieGameComplete?.Invoke();
        }
        scoreText.text = allZombiesInScene.Count - activeZombiesInScene.Count  + "/" + allZombiesInScene.Count;
    }


    public void ResetZombies()
    {
        activeZombiesInScene.Clear();
        for (int i = 0; i < allZombiesInScene.Count; i++)
        {
            activeZombiesInScene.Add(allZombiesInScene[i]);
            allZombiesInScene[i].Reset();
            scoreText.text = allZombiesInScene.Count - activeZombiesInScene.Count + "/" + allZombiesInScene.Count;
        }


    }
}
