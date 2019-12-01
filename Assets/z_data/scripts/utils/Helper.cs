using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LevelID
{
    one,
    two,
    three,
    four,
    five,
    six,
    seven,
    eight,
    nine,
    ten,
    eleven,
    twelve,
    thirteen,
    fourteen,
    fifteen
}

// static class used in game, declare common functions here
public class Helper
{
    public static readonly string SPLASH_SCENE_NAME = "splash";
    public static readonly string GAME_SCENE_NAME = "game";

    public static bool IsInternetAvailabe()
    {
        if (Application.internetReachability != NetworkReachability.NotReachable)
            return false;
        return true;
    }

    public static Quaternion LookAtMouse(Transform _object, Camera _cam)
    {
        var dir = Input.mousePosition - _cam.WorldToScreenPoint(_object.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public static Vector3 GetDirectionTowardsMouse(Transform _object, Camera _cam)
    {
        return Input.mousePosition - _cam.WorldToScreenPoint(_object.position);
    }
}