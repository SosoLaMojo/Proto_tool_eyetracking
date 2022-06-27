using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnterName : MonoBehaviour
{
    public TextMeshProUGUI user_Name;
    public TMP_InputField user_Inputfield;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetName()
    {
        user_Name.text = user_Inputfield.text;
        Debug.Log(user_Name);
        
    }
}
