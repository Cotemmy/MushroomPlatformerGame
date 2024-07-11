using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System.Collections;

public class TimerScript : MonoBehaviour
{
    public bool timerOn = false;
    public float timeRemaining = 60f; // Initialize with a default value
    public TextMeshProUGUI timerText;

    void Start()
    {
        if (PlayerPrefs.HasKey("timeValue"))
        {
            timeRemaining = PlayerPrefs.GetFloat("timeValue");
        }
    }

    void Update()
    {
        if (timerOn)
        {
            timeRemaining += Time.deltaTime;
            PlayerPrefs.SetFloat("timeValue", timeRemaining);
            UpdateTimer(timeRemaining);
        }
    }

    void UpdateTimer(float currentTime)
    {
        int minutes = Mathf.FloorToInt(currentTime / 60);
        int seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("timeValue");
        if (DBManager.LoggedIn)
        {
            StartCoroutine(SendTimerValueToServer(timerText.text)); // Send the formatted timer text
        }
    }

    IEnumerator SendTimerValueToServer(string formattedTime)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", DBManager.username);
        form.AddField("timerValue", formattedTime);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/timer.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                if (!string.IsNullOrEmpty(www.downloadHandler.text))
                {
                    if (www.downloadHandler.text[0] == '0')
                    {
                        Debug.Log("Timer value successfully updated.");
                    }
                    else
                    {
                        Debug.Log("Failed to update timer value. Error: " + www.downloadHandler.text);
                    }
                }
                else
                {
                    Debug.Log("Empty response received from the server.");
                }
            }
            else
            {
                Debug.Log("Network error: " + www.error);
            }
        }
    }
}