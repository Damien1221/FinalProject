using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string NextSceneName = "";

    public void iDie(GameObject obj)
    {
        if(obj.name == "Player")
        {
            SceneManager.LoadScene(NextSceneName);
        }

       Debug.Log(obj.name + " died");    
    }

}
