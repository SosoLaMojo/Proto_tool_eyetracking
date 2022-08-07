using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counter_leaves : MonoBehaviour
{
    [SerializeField] List<GameObject> _leaves;
    [SerializeField] private GameObject _panelMenuStartGame1panelWin;
    [SerializeField] private tracker _tracker;
    int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in _leaves)
        {
            item.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Feuille")
                {
                    Destroy(hit.transform.gameObject);
                    _leaves[counter++].SetActive(true);
                }
            }
        }

        if(counter == 9)
        {
            _panelMenuStartGame1panelWin.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    // Fonction qui permet de sauvegarder la liste de position et de temps de l'eyetracker dans le GameManager
    public void SaveMiniGame()
    {
        // récupère les saveData du GameManager
        SaveData saveData = GameManager.instance.saveData;
        // récupère les timePos du tracker.cs
        var timePos = _tracker.timePos;
        // Creation une list d'event
        List<GameEvent> eyesEvent = new List<GameEvent>();
        // boucle qui permet de remplir la liste eyesEvent des positions et des temps de l'eyetracker
        for (int i = 0; i < timePos.Count; i++)
        {
            eyesEvent.Add(new GameEvent
            {
                eventType = EventType.EYES,
                position = timePos[i].Positions,
                time = timePos[i].time,

            });
        }
        // enregistre la liste eyesEvent dans gameLeaves qui correspond auy donées de la boucle
        saveData.gameLeaves.events = eyesEvent;
        // écrit dans le json les positions et les temps de la liste eyesEvent
        string json = JsonUtility.ToJson(saveData, true);
        GameManager.instance.WriteToFile(json);
    }

    public void MiniGameAnalyze()
    {
        GameManager.instance.ReadJsonGazePoint();
    }
}
