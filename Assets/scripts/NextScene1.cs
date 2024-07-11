using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextscene1 : MonoBehaviour
{
    public string scenename;
    void Start()
    {
    
    }

    void OnTriggerEnter(Collider other)
    {
        
     SceneManager.LoadScene(scenename);
          
    }

}

