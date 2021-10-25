using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject DialogueObj;
    public GameObject HotBarObj;

    private void Update()
    {
        if(DialogueObj.activeSelf)
        {
            HotBarObj.SetActive(false);
        }
        else
        {
            HotBarObj.SetActive(true);
        }
    }
}
