using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tracker : MonoBehaviour
{
    [Serializable]
    struct TimePos
    {
        public Vector2 Positions;
        public float time;
    }
    
    [SerializeField] List<TimePos> timePos;

    void Start()
    {
        
    }

    void Update()
    {
        // retourne la position et le temps
        TimePos newTimePos = new TimePos();
        newTimePos.Positions = InputPosition();
        newTimePos.time = Time.deltaTime;

        if(timePos.Count >= 1)
        {
            // rassembler le temps des positions identiques qui se suivent
            if(newTimePos.Positions == timePos[timePos.Count - 1].Positions)
            {
                newTimePos.time += timePos[timePos.Count - 1].time;
                timePos[timePos.Count - 1] = newTimePos;
            }
            else
            {
                // stock les positions des inputs (pour le moment la souris)
                timePos.Add(newTimePos);
            }
        }
        else
        {
            timePos.Add(newTimePos);
        }
        
    }

    // Fonction qui retourne la position des inputs (la souris)
    public Vector2 InputPosition()
    {
        return Input.mousePosition;
    }
}
