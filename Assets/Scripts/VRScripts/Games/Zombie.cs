using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{

    public Animator animator;
    public BoxCollider collider;
    public ZombieManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<ZombieManager>();
        manager.allZombiesInScene.Add(this);
        manager.activeZombiesInScene.Add(this);
        collider = GetComponent<BoxCollider>();
        animator = GetComponent<Animator>();
       // KillZombie();    
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "NerfDart")
        {
            KillZombie();
        }
    }

    void KillZombie()
    {
        animator.SetBool("Dead", true);
        collider.enabled = false;
        // score a point etc.
        manager.OnZombieKilled(this);
    }

    public void Reset()
    {
        animator.SetBool("Dead", false);
        collider.enabled = true;
    }
}
