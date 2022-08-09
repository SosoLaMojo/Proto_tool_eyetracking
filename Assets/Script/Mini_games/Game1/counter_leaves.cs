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
        // quand le joueur aura cliqu� dessus dans le jeu en les cachant au
        // d�marage de la sc�ne
        foreach (var item in _leaves)
        {
            item.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Condition qui permet de compter les nombre de feuilles qui ont �t� trouv�es,
        // ainsi que de savoir laquelle en passant par un tag

        // Si le bouton le bouton de la souris est cliqu�
        if (Input.GetMouseButtonDown(0))
        {
            // cr�ation d'un raycast � la position de la souris
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            // Si le raycast a toucher un objet...
            if (Physics.Raycast(ray, out hit))
            {
                // .. et si l'objet a pour tag "Feuille, il d�truit le gameObject feuille contenu dans la sc�ne, ajoute 1 au compteur et active la feuille dans l'UI
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
    // dans le GameManager (appel�e dans un bouton de la sc�ne)
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
        // enregistre la liste eyesEvent dans gameLeaves qui correspond aux donn�es de la boucle
        saveData.gameLeaves.events = eyesEvent;
        // �crit dans le json les positions et les temps de la liste eyesEvent
        GameManager.instance.WriteToFile(saveData);
    }
    
    // Fonction qui permet de lire le Json dans la partie Analyse du jeu (appel� dans un bouton de la sc�ne)
    public void MiniGameAnalyze()
    {
        GameManager.instance.ReadJsonGazePoint();
    }
}
