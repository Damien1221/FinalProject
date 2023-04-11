using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text ScoreDisplay;
    public int CurrentScore = 0;
    public GameObject Gate;
    public GameObject Gate2;
    public GameObject Gate3;
    private AudioSource _audioSource;

    public AudioClip audioClip;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    private void Update()
    {
        if (ScoreDisplay == null)
            return;

        ScoreDisplay.text = CurrentScore.ToString();
    }

    public void AddScore(int scoreAmount)
    {
        CurrentScore += scoreAmount;

        if (ScoreDisplay != null)
        {
            if (_audioSource != null)
            {
                _audioSource.PlayOneShot(audioClip);
            }
        }

        if (CurrentScore == 1)
        {
            Destroy(Gate2);
        }
        if (CurrentScore == 3)
        {
            Destroy(Gate3);
        }
        if (CurrentScore == 5)
        {
            Destroy(Gate);
        }
    }
}
