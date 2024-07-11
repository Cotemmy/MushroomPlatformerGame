using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SampleCollector : MonoBehaviour
{
    private int sampleCount = 0;
    public TextMeshProUGUI samplesText;
    private static SampleCollector instance;

    // Store original positions of collectible objects
    private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();
    private List<GameObject> collectedObjects = new List<GameObject>(); // Store collected objects

    void Awake()
    {
        instance = this;
    }

    public static SampleCollector GetInstance()
    {
        return instance;
    }

    void Start()
    {
        samplesText.text = "Зразків грибів: 0";

        // Store original positions of collectible objects
        GameObject[] sampleObjects = GameObject.FindGameObjectsWithTag("Sample");
        foreach (GameObject obj in sampleObjects)
        {
            if (obj != null) // Check if object still exists
            {
                originalPositions[obj] = obj.transform.position;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sample"))
        {
            GameObject sampleObject = other.gameObject;
            if (!collectedObjects.Contains(sampleObject))
            {
                sampleCount++;
                samplesText.text = "Зразків грибів: " + sampleCount.ToString();
                collectedObjects.Add(sampleObject);
                sampleObject.SetActive(false); // Hide the object
            }
        }
    }
    public int GetSampleCount()
    {
    return sampleCount;
    }
    
    public void ResetSamples()
    {
        sampleCount = 0;
        samplesText.text = "Зразків грибів: 0";

        // Respawn collected objects
        foreach (GameObject obj in collectedObjects)
        {
            obj.SetActive(true); // Make the object visible again
            obj.transform.position = originalPositions[obj]; // Reset its position
        }
        collectedObjects.Clear(); // Clear the list of collected objects
    }
}