using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnScript : MonoBehaviour
{
   [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
    private SampleCollector sampleCollector;

    void Start()
    {
        sampleCollector = SampleCollector.GetInstance();
        if (sampleCollector == null)
        {
            Debug.LogWarning("SampleCollector not found!");
        }
    }

    public void RespawnPlayer()
    {
        player.position = respawnPoint.position;
        Physics.SyncTransforms();

        if (sampleCollector != null)
        {
            sampleCollector.ResetSamples();
        }
        else
        {
            Debug.LogWarning("SampleCollector not set!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RespawnPlayer();
        }
    }
}

