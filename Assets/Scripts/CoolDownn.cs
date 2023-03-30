using System.Collections;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

[Serializable]
public class CoolDownn 
{
    public enum Progress
    {
        Ready,
        Started,
        Inprogress,
        Finished
    }

    public Progress CurrentProgress = Progress.Ready;

    public float Duration = 1.0f;
    public float TimeLeft
    {
        get { return _currentDuration; }
    }

    public bool IsOnCooldown
    {
        get { return _isOnCooldown; }
    }

    private float _currentDuration = 0f;

    private bool _isOnCooldown = false;

    private Coroutine _coroutine;

    public void StartCoolDown()
    {
        if (CurrentProgress is Progress.Started or Progress.Inprogress)
            return;

        _coroutine = CoroutineHost.Instance.StartCoroutine(IsOnCoolldown());
    }

    public void StopCoolDown()
    {
        if (_coroutine != null)
            CoroutineHost.Instance.StopCoroutine(_coroutine);

        _currentDuration = 0f;
        _isOnCooldown = false;
        CurrentProgress = Progress.Ready;
    }

    IEnumerator IsOnCoolldown()
    {
        CurrentProgress = Progress.Started;
        _currentDuration = Duration;
        _isOnCooldown = true;

        while (_currentDuration > 0)
        {
            _currentDuration -= Time.deltaTime;
            CurrentProgress = Progress.Inprogress;

            yield return null;

        }

        _currentDuration = 0f;
        _isOnCooldown = false;

        CurrentProgress = Progress.Finished;
    }
}
