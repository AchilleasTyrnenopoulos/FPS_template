using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
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
        
    }

    private void FreezeTime()
    {
        Debug.Log("freezed time");
        Time.timeScale = 0f;
    }

    private void ResumeNormalTime()
    {
        Time.timeScale = 1f;
    }
}
