using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class NextLevel : MonoBehaviour
{
    public string NextSceneName = "";
    public float time = 2f;

    public AudioClip audioClip;
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Countdown");
        SceneManager.LoadScene(NextSceneName);
    }

    
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.gameObject.CompareTag("Player"))
            return;
        if (col.gameObject.CompareTag("Player"))
        {
            Destroy(col.gameObject);
            _audioSource.PlayOneShot(audioClip);
            StartCoroutine(Countdown());
        }
    }

}
