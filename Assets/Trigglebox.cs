using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Trigglebox : MonoBehaviour
{
    public GameObject Box;

    public GameObject box2;

    public float time = 3f;
    public float timeofsound = 2f;
    public AudioClip audioClip;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(time);
        Destroy(box2);
        _audioSource.PlayOneShot(audioClip);
        yield return new WaitForSeconds(timeofsound);
        Destroy(_audioSource);
    }
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.gameObject.CompareTag("Player"))
            return;

        if(col.gameObject.CompareTag("Player"))
        {
            Destroy(Box);
            StartCoroutine(Countdown());
        }
        if (_audioSource != null)
        {
            return;
        }
    }





}
