using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class nextscene : MonoBehaviour
{
    public string scenename;
    public TextMeshProUGUI messageText; // Reference to the TextMeshPro object
    private SampleCollector sampleCollector; // Reference to the SampleCollector script
    public int requiredSamples; // Number of samples required to move to the next scene

    void Start()
    {
        sampleCollector = SampleCollector.GetInstance();
        if (sampleCollector == null)
        {
            Debug.LogWarning("SampleCollector not found!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the number of collected samples equals the required number
            if (sampleCollector.GetSampleCount() >= requiredSamples)
            {
                SceneManager.LoadScene(scenename);
            }
            else
            {
                // Display message on TextMeshPro object
                StartCoroutine(DisplayMessage("Ви не зібрали усі зразки грибів"));
            }
        }
    }

    IEnumerator DisplayMessage(string message)
    {
        // Set the message text
        messageText.text = message;
        
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Clear the message text
        messageText.text = "";
    }
}

