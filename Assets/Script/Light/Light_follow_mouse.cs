using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_follow_mouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FollowMousePosition();
    }

    private void FollowMousePosition()
    {
        var mousePos = Input.mousePosition;
        var wantedPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
        wantedPos.z = 0;
        transform.position = wantedPos;
    }
}
