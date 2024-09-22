using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDisplay : MonoBehaviour
{
    //making a public array of heartUi
    //you will need to refrence all 4 heart sprites in the inspector
    public GameObject[] heartUi;
    //making int i which will be used as index of array
    public int i;

    void Start()
    {

    }

    void Update()
    {
        //decrease health
        //whatever condition you want in the if statement just dont remove  && i >= 0 part
        if (Input.GetKeyDown(KeyCode.F) && i >= 0)
        {
            heartUi[i].SetActive(false);
            i--;
        }

        //increse health
        //whatever condition you want in the if statement just dont remove  && i < 3 part
        if (Input.GetKeyDown(KeyCode.R) && i < 2)
        {
            i++;
            heartUi[i].SetActive(true);
        }
    }
}
