using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class EnterName : MonoBehaviour
{
    public TextMeshProUGUI user_Name;
    public TMP_InputField user_Inputfield;

    // Fonction qui enregistre le nom entré par l'utilisateur
    public void SetName()
    {
        user_Name.text = user_Inputfield.text;
        GameManager.instance.user_Name = user_Inputfield.text;
    }
}
