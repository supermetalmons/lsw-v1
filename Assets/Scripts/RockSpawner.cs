using System.Collections;
using UnityEngine;
using Photon.Pun;

public class RockSpawner : MonoBehaviourPun
{
    public GameObject RockPrefab;
    public float minDelay = 10f; // Minimum delay in seconds
    public float maxDelay = 12f; // Maximum delay in seconds
    public int maxRocks = 5; // Maximum number of rocks
    public static RockSpawner instance;
    private int currentRocks = 0; // Current number of rocks
    public GameObject RockHolder;

    private void Awake()
    {
        instance = this;
    }

    private IEnumerator SpawnRock()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(2,10));
            if (currentRocks < maxRocks)
            {
                float randomPositionX = Random.Range(-8.5f, 8.5f);
                float randomPositionY = Random.Range(-4f, 4f);
                GameObject go = PhotonNetwork.Instantiate(RockPrefab.name, new Vector3(randomPositionX, randomPositionY, 0), Quaternion.identity);
                go.transform.SetParent(RockHolder.transform);

                currentRocks++; // Increment the rock count
                // Optionally, add a callback or listener to the rock's destruction event to decrement currentRocks
            }
            // Wait for a random time between minDelay and maxDelay
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
        }
    }

    public void RockSpawnerFunction()
    {        
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(SpawnRock());
        }
    }

    // Optional: Method to decrement the rock count when a rock is destroyed
    public void RockDestroyed()
    {
        if (currentRocks > 0)
        {
            currentRocks--;
        }
    }
}
