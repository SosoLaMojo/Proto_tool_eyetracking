using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[Serializable]
public struct SaveData
{
    public GameLeaves gameLeaves;
    public GameSearchAndFind gameSearchAndFind;
    public GameReading gameReading;
}

public enum EventType
{
    EYES,
    CLICK
}

[Serializable]
public class GameEvent
{
    public float time;
    public Vector3 position;
    public EventType eventType;
}

[Serializable]
public struct GameLeaves
{
    public List<GameEvent> events;
}

[Serializable]
public struct GameSearchAndFind
{
    public List<GameEvent> events;
}

[Serializable]
public struct GameReading
{
    public List<GameEvent> events;
}

public class GameManager : MonoBehaviour
{
    public SaveData saveData;

    public string user_Name;
    private bool isRunning = false;

    public static GameManager instance;

    [SerializeField] GameObject point;
    [SerializeField] LineRenderer line;

    private void Awake()
    {
        // Patern singleton pour s'assurer qu'il n'y a qu'un seul GameManager dans toutes les scènes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Fonction qui permet de créer un Json
    public void CreateJsonGazePoint()
    {
        SaveData saveData = new SaveData();
        string json = JsonUtility.ToJson(saveData, true);
        WriteToFile(json);
        //Debug.Log(json);
    }

    // Fonction qui permet d'écrire le Json
    public void CreateJsonFile()
    {
        FileStream fileStream = new FileStream($"{ GameManager.instance.user_Name }.txt", FileMode.Create);
    }

    public void WriteToFile(string json)
    {
        FileStream fileStream = new FileStream($"{ GameManager.instance.user_Name }.txt", FileMode.Open);
        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    // Fonction qui permet de lire le Json
    public SaveData ReadJsonGazePoint()
    {
        // Lecture du json
        string json;
        StreamReader reader = new StreamReader($"{ GameManager.instance.user_Name }.txt", true);
        json = reader.ReadToEnd();

        // Traduit le string en structure SaveData
        saveData = JsonUtility.FromJson<SaveData>(json);

        return saveData;
    }

    // Fonction qui permet d'afficher le gazepoint du Json
    public void DrawJsonGazePoint()
    {
        // supprime les gazepoint préexistants
        int childs = transform.childCount;
        //for (int i = 0; i < childs; i++)
        //{
        //    GameObject.Destroy(transform.GetChild(i).gameObject);
        //}

        // Lecture des positions
        SaveData saveData;
        saveData = ReadJsonGazePoint();

        // Instantiation du gazepoint contenu dans le Json
        if (isRunning == false)
        {
            StartCoroutine(Wrapper(saveData));
        }
    }

    IEnumerator Wrapper(SaveData saveData)
    {
        float timeOffset = saveData.gameLeaves.events[0].time;
        isRunning = true;
        for (int i = 0; i < saveData.gameLeaves.events.Count; i++)
        {
            if(saveData.gameLeaves.events[i].eventType == EventType.EYES)
            {
                Vector2 positionGazePoint = saveData.gameLeaves.events[i].position;
                GameObject.Instantiate(point, positionGazePoint, Quaternion.identity, transform);
                line.positionCount = i + 1;
                line.SetPosition(i, positionGazePoint);
                yield return new WaitForSeconds(saveData.gameLeaves.events[i].time - timeOffset);
                timeOffset = saveData.gameLeaves.events[i].time;
            }
        }
        isRunning = false;
    }
}
