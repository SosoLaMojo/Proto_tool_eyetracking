using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DentedPixel;

public class Scene_Manager : MonoBehaviour
{
    // Game 1
    [SerializeField] private GameObject panelMenuStartGame1;
    private AudioSource audioSource;
    public GameObject bar;
    public float time;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void DesactivatePanelStartGame1()
    {
        panelMenuStartGame1.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void PlayAudio(string audio)
    {
        audioSource.Play();
    }
}
