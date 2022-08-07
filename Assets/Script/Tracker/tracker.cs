using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Tobii.Gaming;
using System.IO;

public class tracker : MonoBehaviour
{
    [Serializable]
    public struct TimePos
    {
        public Vector2 Positions;
        public float time;
    }

    [Serializable]
    struct SaveData
    {
        public List<TimePos> timePos;
    }
    
    public List<TimePos> timePos;
    //[SerializeField] GameObject point;
    //[SerializeField] LineRenderer line;

    public List<Vector2> positionList = new List<Vector2>();
    public float timer;
    bool isRecording = false;
    //private bool isRunning = false;
    //bool pointDisplay = true;


    void Start()
    {
        Debug.Log(InputSystem.devices);
    }

    void Update()
    {
        //pointDisplay = false;
        if (!isRecording) return;
        positionList.Add(InputPosition());
        timer += Time.deltaTime;
        if (timer >= 0.2)
        {
            // enregistre les position toutes les secondes
            TimePos newTimePos = new TimePos();
            newTimePos.Positions = InputPosition();
            newTimePos.time = Time.time;
            // fait la moyenne
            Vector2 sum = Vector2.zero;
            for (int i = 0; i < positionList.Count; i++)
            {
                sum += positionList[i];
            }
            sum /= positionList.Count;
            newTimePos.Positions = sum;
            timePos.Add(newTimePos);
        
            //if (pointDisplay = false)
            //{
            //    GameObject.Instantiate(point, newTimePos.Positions, Quaternion.identity, transform);
            //}
            timer = 0;
            positionList.Clear();
        }
    }

    //Fonction qui retourne la position des inputs(eyetracker)
    public Vector2 InputPosition()
    {
        GazePoint gazePoint = TobiiAPI.GetGazePoint();
        return Camera.main.ScreenToWorldPoint(gazePoint.Screen);
    }

    public void Recording()
    {
        isRecording = true;
    }

    // Fonction qui permet de créer un Json
    //public void CreateJsonGazePoint()
    //{
    //    SaveData saveData = new SaveData();
    //    saveData.timePos = timePos;
    //    string json = JsonUtility.ToJson(saveData, true);
    //    WriteToFile(json);
    //    Debug.Log(json);
    //}

    // Fonction qui permet d'écrire dans le Json
    //private void WriteToFile(string json)
    //{
    //    //FileStream fileStream = new FileStream($"{ GameManager.instance.user_Name }.txt", FileMode.Create);
    //    FileStream fileStream = new FileStream($"{ GameManager.instance.user_Name }.txt", FileMode.Open);
    //    using (StreamWriter writer = new StreamWriter(fileStream))
    //    {
    //        writer.Write(json);
    //    }
    //}

    // Fonction qui permet de lire le Json
    //private SaveData ReadJsonGazePoint()
    //{
    //    // Lecture du json
    //    string json;
    //    StreamReader reader = new StreamReader($"{ GameManager.instance.user_Name }.txt", true);
    //    json = reader.ReadToEnd();
    //    Debug.Log(json);
    //    // Traduit le string en structure SaveData
    //    SaveData saveData = new SaveData();
    //    saveData = JsonUtility.FromJson<SaveData>(json);

    //    return saveData;
    //}

    // Fonction qui permet d'afficher le gazepoint du Json
    //public void DrawJsonGazePoint()
    //{
    //    // supprime les gazepoint préexistants
    //    int childs = transform.childCount;
    //    for (int i = 0; i < childs; i++)
    //    {
    //        GameObject.Destroy(transform.GetChild(i).gameObject);
    //    }

    //    // Lecture des positions
    //    SaveData saveData;
    //    saveData = ReadJsonGazePoint();

    //    // Instantiation du gazepoint contenu dans le Json
    //    if (isRunning == false)
    //    {
    //        StartCoroutine(Wrapper(saveData));
    //    }
    //}

    //IEnumerator Wrapper(SaveData saveData)
    //{
    //    isRunning = true;
    //    for (int i = 0; i < saveData.timePos.Count; i++)
    //    {
    //        Vector2 positionGazePoint = saveData.timePos[i].Positions;
    //        GameObject.Instantiate(point, positionGazePoint, Quaternion.identity, transform);
    //        line.positionCount = i + 1;
    //        line.SetPosition(i, positionGazePoint);
    //        yield return new WaitForSeconds(saveData.timePos[i].time);
    //    }
    //    isRunning = false;
    //}
}
