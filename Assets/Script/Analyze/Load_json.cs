using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Load_json : MonoBehaviour
{
    // Fonction qui charge le Json
    public void LoadFileName()
    {
        GameManager.instance.ReadJsonGazePoint();
    }

    // Fonction qui dessine les gazepoint
    public void DrawGazePoint()
    {
        GameManager.instance.DrawJsonGazePoint();
    }
}
