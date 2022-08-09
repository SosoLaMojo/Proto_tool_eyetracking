using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_stories1_tracker : MonoBehaviour
{
    [SerializeField] private tracker _tracker;
    [SerializeField] int page;

    private void Start()
    {
        Time.timeScale = 1;
        // condition, si on est en mode jeu on lance le recording du tracker
        if (_tracker != null)
        {
            _tracker.Recording();
        }
        // si on est en mode analyse, je lis le Json et dessine le Gazepoint en fonction de la currentpage
        else
        {
            MiniGameAnalyze();
            GameManager.instance.DrawJsonGazePoint(page);
        }
    }
    public void SaveMiniGame()
    {
        // récupère les saveData du GameManager
        SaveData saveData = GameManager.instance.saveData;
        // récupère les timePos du tracker.cs
        var timePos = _tracker.timePos;
        // Creation une liste d'event
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
        // enregistre la liste eyesEvent dans gameReading qui correspond aux donées de la boucle
        GameReading gameReading = new GameReading();
        gameReading.events = eyesEvent;
        // rajout d'une page dans les saveData dans la classe GameReading
        saveData.gameReading1.Add(gameReading);
        // écrit dans le json les positions et les temps de la liste eyesEvent
        GameManager.instance.WriteToFile(saveData);
    }

    // Fonction qui permet de lire le Json dans la partie Analyse du jeu (appelé dans un bouton de la scène)
    public void MiniGameAnalyze()
    {
        GameManager.instance.ReadJsonGazePoint();
    }
}
