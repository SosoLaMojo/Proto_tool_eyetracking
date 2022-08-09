using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save_stories2_tracker : MonoBehaviour
{
    [SerializeField] private tracker _tracker;
    [SerializeField] int page;

    private void Start()
    {
        Time.timeScale = 1;
        if (_tracker != null)
        {
            _tracker.Recording();
        }
        else
        {
            MiniGameAnalyze();
            GameManager.instance.DrawJsonGazePoint(page);
        }
    }
    public void SaveMiniGame()
    {
        // r�cup�re les saveData du GameManager
        SaveData saveData = GameManager.instance.saveData;
        // r�cup�re les timePos du tracker.cs
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
        // enregistre la liste eyesEvent dans gameLeaves qui correspond aux don�es de la boucle
        GameReading gameReading = new GameReading();
        gameReading.events = eyesEvent;
        saveData.gameReading2.Add(gameReading);
        // �crit dans le json les positions et les temps de la liste eyesEvent
        GameManager.instance.WriteToFile(saveData);
    }

    // Fonction qui permet de lire le Json dans la partie Analyse du jeu (appel� dans un bouton de la sc�ne)
    public void MiniGameAnalyze()
    {
        GameManager.instance.ReadJsonGazePoint();
    }
}
