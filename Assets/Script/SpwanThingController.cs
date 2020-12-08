using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanThingController : MonoBehaviour
{
    // Start is called before the first frame update

    private SpwanObjectRandom spwanObjectRandom;
    public int numSpawnGen;
    void Start()
    {
        spwanObjectRandom = GetComponent<SpwanObjectRandom>();
        for (int i = 0; i < numSpawnGen; i++) 
        {
            spwanObjectRandom.SpawnThing();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
