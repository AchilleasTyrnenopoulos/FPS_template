using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject _pauseButtonsGO;
    private void Start()
    {
        EventAggregator.GetEvent<PauseStartEvent>().Subscribe(EnablePauseBtnsGO);
        EventAggregator.GetEvent<PauseEndEvent>().Subscribe(DisablePauseBtnsGO);
    }

    private void OnDisable()
    {
        EventAggregator.GetEvent<PauseStartEvent>().UnSubscribe(EnablePauseBtnsGO);
        EventAggregator.GetEvent<PauseEndEvent>().UnSubscribe(DisablePauseBtnsGO);
    }

    private void EnablePauseBtnsGO()
    {
        _pauseButtonsGO.SetActive(true);
    }

    private void DisablePauseBtnsGO()
    {
        _pauseButtonsGO.SetActive(false);
    }



}
