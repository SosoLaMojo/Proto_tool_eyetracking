using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DentedPixel;
using TMPro;
using UnityEngine.UI;

public class Scene_Manager : MonoBehaviour
{
    // Game 1
    [SerializeField] private GameObject panelMenuStartGame1;
    private AudioSource audioSource;
    public GameObject bar;
    public float time;

    string user_Name;
    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private Button levelToButton;

    private void Start()
    {
        // condition qui permet de d'afficher le nom de l'utilisateur dans le menu des min-jeux
        if(name != null)
        {
            user_Name = GameManager.instance.user_Name;
            name.text = user_Name;
        }
        // condition qui permet de débloquer le bouton du mini-jeu 2
        if(levelToButton != null)
        {
            levelToButton.interactable = GameManager.instance.isLevelTwoUnlocked;
        }
    }

    public void LoadScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
        if(scene_name == "Leaves")
        {
            panelMenuStartGame1.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void AnimateBar()
    {
        LeanTween.scaleX(bar, 1, time);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void DesactivatePanelStartGame1(/*string scene_name*/)
    {
        panelMenuStartGame1.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void PlayAudio(string audio)
    {
        audioSource.Play();
    }

    // fonction qui retourne true si le timer ne s'est pas totalement écoulé
    public bool isTimeGood()
    {
        return bar.transform.localScale.x != 1;
    }
}
