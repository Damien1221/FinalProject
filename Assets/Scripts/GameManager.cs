using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public string NextSceneName = "";

    public AudioClip audioClip;
    public AudioClip enemydie;
    public float time = 3f;

    private AudioSource _audioSource;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            BackMenu();
        }
    }

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Loading new scene");
        SceneManager.LoadScene(NextSceneName);
    }
    private void BackMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void iDie(GameObject obj)
    {
        
        if (obj.gameObject.CompareTag("Player"))
        {
            _audioSource.PlayOneShot(audioClip);
            StartCoroutine(Countdown());
        }

        if(obj.gameObject.CompareTag("Enemy"))
        {
            _audioSource.PlayOneShot(enemydie);
        }
       
        Debug.Log(obj.name + " died");    
    }
    
}
