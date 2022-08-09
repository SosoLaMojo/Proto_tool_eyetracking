using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counter_leaves : MonoBehaviour
{
    [SerializeField] List<GameObject> _leaves;
    [SerializeField] private GameObject _panelMenuStartGame1panelWin;
    [SerializeField] private tracker _tracker;
    int counter = 0;
    [SerializeField] Scene_Manager _sceneManager;

    // Start is called before the first frame update
    void Start()
    {
        // permet de stocker les feuilles qui devront s'afficher dans l'UI
        // quand le joueur aura cliqué dessus dans le jeu en les cachant au
        // démarage de la scène
        foreach (var item in _leaves)
        {
            item.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Condition qui permet de compter les nombre de feuilles qui ont été trouvées,
        // ainsi que de savoir laquelle en passant par un tag

        // Si le bouton le bouton de la souris est cliqué
        if (Input.GetMouseButtonDown(0))
        {
            // création d'un raycast à la position de la souris
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Si le raycast a toucher un objet...
            if (Physics.Raycast(ray, out hit))
            {
                // .. et si l'objet a pour tag "Feuille, il détruit le gameObject feuille contenu dans la scène, ajoute 1 au compteur et active la feuille dans l'UI
                if (hit.transform.gameObject.tag == "Feuille")
                {
                    Destroy(hit.transform.gameObject);
                    _leaves[counter++].SetActive(true);
                }
            }
        }

        // Quand le compteur feuilles atteint 9, le panel de Win s'affiche
        if(counter == 9)
        {
            _panelMenuStartGame1panelWin.gameObject.SetActive(true);
            Time.timeScale = 0;

            // change le bool a true si le mini-jeu 1 est fini dans le temps impartit
            if (_sceneManager.isTimeGood())
            {
                GameManager.instance.isLevelTwoUnlocked = true;
            }
        }
    }

    // Fonction qui permet de sauvegarder la liste de position et de temps de l'eyetracker
    // dans le GameManager (appelée dans un bouton de la scène)
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
        // enregistre la liste eyesEvent dans gameLeaves qui correspond aux données de la boucle
        saveData.gameLeaves.events = eyesEvent;
        // écrit dans le json les positions et les temps de la liste eyesEvent
        GameManager.instance.WriteToFile(saveData);
    }
    
    // Fonction qui permet de lire le Json dans la partie Analyse du jeu (appelé dans un bouton de la scène)
    public void MiniGameAnalyze()
    {
        GameManager.instance.ReadJsonGazePoint();
    }
}
