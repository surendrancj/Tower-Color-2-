using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SplashScreen : MonoBehaviour
{
    void Start()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0f;
    }

    public void GotoMenu()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Screen.fullScreen = true;
        SceneManager.LoadScene(Helper.GAME_SCENE_NAME);
    }
}