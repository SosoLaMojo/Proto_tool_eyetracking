using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DentedPixel;
using TMPro;
using UnityEngine.UI;

public class Scene_Manager : MonoBehaviour
{
    [SerializeField] private GameObject panelMenuStartGame1;
    private AudioSource audioSource;
    public GameObject bar;
    public float time;

    string user_Name;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private Button levelToButton;

    private void Start()
    {
        // condition qui permet de d'afficher le nom de l'utilisateur dans le menu des mini-jeux (a mettre dans un script Menu_Manager)
        if(name != null)
        {
            user_Name = GameManager.instance.user_Name;
            name.text = user_Name;
        }
        // condition qui permet de débloquer le bouton du mini-jeu 2(a mettre dans un script Menu_Manager)
        if (levelToButton != null)
        {
            levelToButton.interactable = GameManager.instance.isLevelTwoUnlocked;
        }
    }

    // fonction qui permet de chargé une nouvelle scène par l'intermédiaire d'un bouton
    public void LoadScene(string scene_name)
    {
        GameManager.instance.Clear();
        SceneManager.LoadScene(scene_name);
    }

    // Fonction qui permet de lancer l'animation du timer dans le jeu counter_leaves (a mettre dans le script counter_leaves)
    public void AnimateBar()
    {
        LeanTween.scaleX(bar, 1, time);
    }

    // Fonction qui permet de quitter l'application
    public void Quit()
    {
        Application.Quit();
    }

    // fonction qui permet de desactiver un panel
    public void DesactivatePanelStartGame1()
    {
        panelMenuStartGame1.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    // fonction qui permet de lancer un audio
    public void PlayAudio(string audio)
    {
        audioSource.Play();
    }

    // fonction qui retourne true si le timer ne s'est pas totalement écoulé ( a laisser avec la fonction AnimateBar() dans le script counter_leaves
    public bool isTimeGood()
    {
        return bar.transform.localScale.x != 1;
    }
}
