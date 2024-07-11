using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public TMP_InputField usernamefield;
    public TMP_InputField passwordfield;
    public Button submitButton;

    public void CallLogin()
    {
        StartCoroutine(LoginPlayer());
    }

    IEnumerator LoginPlayer()
    {
        // Create a form with username and password fields
        WWWForm form = new WWWForm();
        form.AddField("username", usernamefield.text);
        form.AddField("password", passwordfield.text);

        // Send login request to the server
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/login.php", form))
        {
            yield return www.SendWebRequest(); // Wait for the server response

            if (www.result == UnityWebRequest.Result.Success)
            {
                // Check if the response text is not empty
                if (!string.IsNullOrEmpty(www.downloadHandler.text))
                {
                    // Check the length of the response text to ensure it's valid
                    if (www.downloadHandler.text.Length > 0 && www.downloadHandler.text[0] == '0')
                    {
                    DBManager.username = usernamefield.text;
                    Debug.Log("User logged in successfully.");

                    // Reload the scene
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene("Main Menu");
                        // Example: DBManager.score = int.Parse(www.downloadHandler.text.Split('\t')[1]);
                    }
                    else
                    {
                        // Login failed, display error message from server
                        Debug.Log("User login failed. Error: " + www.downloadHandler.text);
                    }
                }
                else
                {
                    // Empty response received, log an error message
                    Debug.Log("Empty response received from the server.");
                }
            }
            else
            {
                // Network or server error occurred
                Debug.Log("Network error: " + www.error);
            }
        }
    }

    public void VerifyInputs()
    {
        // Enable the submit button only if both fields meet the length requirements
        submitButton.interactable = (usernamefield.text.Length >= 4 && passwordfield.text.Length >= 8);
    }
}
