using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Load_json : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadFileName()
    {
        GameManager.instance.ReadJsonGazePoint();
    }

    public void DrawGazePoint()
    {
        GameManager.instance.DrawJsonGazePoint();
    }
}
