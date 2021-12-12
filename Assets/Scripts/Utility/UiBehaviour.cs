/*
Nathan Nguyen
George Brown College
Assignment 2 - GAME2014-F2021

101268067
10/23/2021

Description:
Handles all UI Button Interactions

*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiBehaviour : MonoBehaviour
{
    private int nextSceneIndex;
    private int previousSceneIndex;



    void Start()
    {

        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
    }

    public void OnPauseButtonPressed(GameObject PauseMenu)
    {
        Time.timeScale = 0.0f;
        PauseMenu.SetActive(true);
    }

    public void OnResumeButtonPressed(GameObject PauseMenu)
    {
        Time.timeScale = 1.0f;
        PauseMenu.SetActive(false);
    }

    public void Test()
    {
        Debug.Log("Test Successful");
    }


    public void OnExitButtonPressed()
    {
        Application.Quit();  
    }


    public void ButtonGeneralPressed(string SceneSelect)
    {
        SceneManager.LoadScene(SceneSelect);
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }


    public void CanvasSwitch(GameObject canvas)
    {
        canvas.SetActive(!canvas.activeSelf);
    }


    public void ButtonGeneralRestartTimePressed(string SceneSelect)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneSelect);
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
    }


}
