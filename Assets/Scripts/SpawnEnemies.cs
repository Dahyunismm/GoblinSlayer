using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnEnemies : MonoBehaviour
{
    public TextMeshProUGUI triggerText;
    private PlayerData data;
    public GameObject Parent;
    public GameObject goblin;
    public GameObject spawner1;
    public GameObject spawner2;
    public GameObject spawner3;
    public Transform spawnerPos1;
    public Transform spawnerPos2;
    public Transform spawnerPos3;

    private void Start()
    {
        spawnerPos1 = spawner1.transform;   
        spawnerPos2 = spawner2.transform;
        spawnerPos3 = spawner3.transform;

        data = FindObjectOfType<PlayerData>();
        triggerText = data.triggertext;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            triggerText.gameObject.SetActive(true);
            triggerText.text = "Challenge the El Goblino";
            if (Input.GetKeyDown(KeyCode.C))
            {
                Instantiate(goblin, spawnerPos1.transform.position, Quaternion.identity);
                Instantiate(goblin, spawnerPos2.transform.position, Quaternion.identity);
                Instantiate(goblin, spawnerPos3.transform.position, Quaternion.identity);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        triggerText.gameObject.SetActive(false);
    }   
}
