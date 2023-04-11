using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string NextSceneName = "";

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player"))
            return;

        Debug.Log(col.gameObject);
        SceneManager.LoadScene(NextSceneName);
    }

}
