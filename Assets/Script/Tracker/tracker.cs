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

    public List<Vector2> positionList = new List<Vector2>();
    public float timer;
    bool isRecording = false;

    void Update()
    {
        // l'update ne se lance que si le tracker enregistre des data
        if (!isRecording) return;
        // Si la condition précédente n'est pas rempli, la position est ajoutée dans la positionList
        positionList.Add(InputPosition());
        timer += Time.deltaTime;
        // Si le timer est plus grand ou égal à 0.2secondes, enregistre la moyenne
        if (timer >= 0.2)
        {
            //enregistre les position toutes les secondes
            TimePos newTimePos = new TimePos();
            newTimePos.Positions = InputPosition();
            newTimePos.time = Time.time;
            // fait la moyenne pour ne pas afficher trop de positions et de temps
            Vector2 sum = Vector2.zero;
            for (int i = 0; i < positionList.Count; i++)
            {
                sum += positionList[i];
            }
            sum /= positionList.Count;
            newTimePos.Positions = sum;
            // ajout de la liste de point dans le timePos
            timePos.Add(newTimePos);
            timer = 0;
            // effacement de la positionList pour recommencer une nouvelle moyenne pour le point suivant 
            positionList.Clear();
        }
    }

    //Fonction qui retourne la position des inputs(eyetracker)
    public Vector2 InputPosition()
    {
        GazePoint gazePoint = TobiiAPI.GetGazePoint();
        return Camera.main.ScreenToWorldPoint(gazePoint.Screen);
    }

    // bool qui permet de lancer l'enregistrement des données de l'eyetracker
    // (appelée dans un bouton de la scène)
    public void Recording()
    {
        isRecording = true;
    }
}
