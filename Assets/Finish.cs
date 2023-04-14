using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Finish : MonoBehaviour
{
    public string NextSceneName = "";

    public AudioClip audioClip;
    
    public float time = 3f;

    private AudioSource _audioSource;
    
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Loading new scene");
        SceneManager.LoadScene(NextSceneName);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Finish"))
        {
            _audioSource.PlayOneShot(audioClip);
            StartCoroutine(Countdown());
        }
    }
}
