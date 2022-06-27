using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class counter_leaves : MonoBehaviour
{
    [SerializeField] List<GameObject> _leaves;
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var item in _leaves)
        {
            item.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Feuille")
                {
                    Destroy(hit.transform.gameObject);
                    _leaves[counter++].SetActive(true);
                }
            }
        }
    }
}
