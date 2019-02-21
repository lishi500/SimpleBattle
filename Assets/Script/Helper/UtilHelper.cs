using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilHelper {

    private UtilHelper() { }
    public static UtilHelper _instance;
    public static UtilHelper Instance {
        get {
            if (_instance == null) {
                _instance = new UtilHelper();
            }
            return _instance;
        }
    }

    public Vector3 worldToUISpace(Canvas parentCanvas, Vector3 worldPos)
    {
        //Convert the world for screen point so that it can be used with ScreenPointToLocalPointInRectangle function
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        Vector2 movePos;

        //Convert the screenpoint to ui rectangle local point
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.transform as RectTransform, screenPos, parentCanvas.worldCamera, out movePos);
        //Convert the local point to world point
        return parentCanvas.transform.TransformPoint(movePos);
    }

    public static void LogTime(string message)
    {
        Debug.Log(string.Format("{0:HH:mm:ss tt}", DateTime.Now) + ": " + message);
    }
}
