using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Registration : MonoBehaviour
{
    public TMP_InputField usernamefield;
    public TMP_InputField passwordfield;
    public Button submitButton;

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Register()
    {
        // Create a form and add the fields
        WWWForm form = new WWWForm();
        form.AddField("username", usernamefield.text);
        form.AddField("password", passwordfield.text);

        // Use UnityWebRequest to send the form
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/sqlconnect/register.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                if (www.downloadHandler.text == "0")
                {
                    Debug.Log("User created successfully.");
                }
                else
                {
                    Debug.Log("User creation failed #" + www.downloadHandler.text);
                }
            }
            else
            {
                Debug.Log("Error: " + www.error);
            }
        }
    }

    public void VerifyInputs()
    {
        // Enable the submit button only if both fields have at least 8 characters
        submitButton.interactable = (usernamefield.text.Length >= 4 && passwordfield.text.Length >= 8);
    }
}
