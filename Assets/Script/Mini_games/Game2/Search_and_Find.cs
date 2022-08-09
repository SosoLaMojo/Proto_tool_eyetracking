using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Search_and_Find : MonoBehaviour
{
    [SerializeField] List<GameObject> _objects;
    GameObject randomItem;
    [SerializeField] Image panelImage;
    [SerializeField] private GameObject _panelMenuStartGame1panelWin;
    [SerializeField] private tracker _tracker;
    private int randomItemIndex;
    private string sceneName = "Cherche_et_trouve";

    // Start is called before the first frame update
    void Start()
    {
        // condition qui permet de choisir un objet en random dans la scène en jeu
        if(sceneName == SceneManager.GetActiveScene().name)
        {
            randomItemIndex = Random.Range(0, _objects.Count - 1);
            randomItem = _objects[randomItemIndex];
            panelImage.sprite = randomItem.GetComponent<SpriteRenderer>().sprite;
        }
        // permet de récupérer l'index de l'item random pour être affiché dans les analyses
        else
        {
            randomItemIndex = GameManager.instance.ReadJsonGazePoint().gameSearchAndFind.randomItem;
            randomItem = _objects[randomItemIndex];
            panelImage.sprite = randomItem.GetComponent<SpriteRenderer>().sprite;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        // condition qui permet de determiner si l'objet random à été cliquer avec la souris
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit.collider != null)
            {
                if(hit.transform.gameObject == randomItem)
                {
                    _panelMenuStartGame1panelWin.gameObject.SetActive(true);
                    Time.timeScale = 0;
                }
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
        // Creation d'une liste d'event
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
        // enregistre la liste eyesEvent dans gameSearchAndFind qui correspond aux données de la boucle
        saveData.gameSearchAndFind.events = eyesEvent;
        // enregistre l'item random qui à été choisit
        saveData.gameSearchAndFind.randomItem = randomItemIndex;
        // écrit dans le json les positions et les temps de la liste eyesEvent
        GameManager.instance.WriteToFile(saveData);
    }

    // Fonction qui permet de lire le Json dans la partie Analyse du jeu (appelé dans un bouton de la scène)
    public void MiniGameAnalyze()
    {
        GameManager.instance.ReadJsonGazePoint();
    }
}
