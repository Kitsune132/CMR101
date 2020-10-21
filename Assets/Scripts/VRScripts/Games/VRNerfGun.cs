using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRNerfGun : MonoBehaviour
{

    public GameObject nerfDartPrefab;
    public float nerfDartForce = 10;
    public float nerfDartDespawnTime = 5;
    public Transform dartSpawnLocation;

    // Start is called before the first frame update

    public void Fire()
    {
        GameObject clone = Instantiate(nerfDartPrefab, dartSpawnLocation.position, dartSpawnLocation.rotation);
        Destroy(clone,nerfDartDespawnTime);
        clone.GetComponent<Rigidbody>().AddForce(dartSpawnLocation.forward * nerfDartForce);
    }
}
