using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] TinyTween logoTween;
    // Start is called before the first frame update
    void Start ()
    {
        logoTween.TweenComplete += GotoMenu;
    }

    public void GotoMenu ()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Screen.fullScreen = true;
        logoTween.TweenComplete -= GotoMenu;
        SceneManager.LoadScene (Helper.MENU_SCENE_NAME);
    }
}