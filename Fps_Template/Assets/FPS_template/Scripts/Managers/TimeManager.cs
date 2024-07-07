using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private bool _countingTime = true;
    [SerializeField]
    private float _counter = 0f;
    private void OnEnable()
    {
        EventAggregator.GetEvent<PauseStartEvent>().Subscribe(FreezeTime);
        EventAggregator.GetEvent<PauseEndEvent>().Subscribe(ResumeNormalTime);
    }

    private void OnDisable()
    {
        EventAggregator.GetEvent<PauseStartEvent>().UnSubscribe(FreezeTime);
        EventAggregator.GetEvent<PauseEndEvent>().UnSubscribe(ResumeNormalTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (_countingTime)
        {
            _counter += Time.deltaTime;
        }
    }

    private void FreezeTime()
    {
        Debug.Log("freezed time");
        Time.timeScale = 0f;
        _countingTime = false;
    }

    private void ResumeNormalTime()
    {
        Time.timeScale = 1f;
        _countingTime = true;
    }
}
