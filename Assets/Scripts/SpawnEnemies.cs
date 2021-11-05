using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace UnityStandardAssets.Characters.FirstPerson
{
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

        /// <summary>
        /// If player is near the spawn banner of enemies, it will prompt a trigger text that allows the player to summon goblins 
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Player")
            {
                triggerText.gameObject.SetActive(true);
                triggerText.text = "To Challenge an El Goblino camp press C";
                if (Input.GetKeyDown(KeyCode.C))
                {
                    Instantiate(goblin, spawnerPos1.transform.position, Quaternion.identity);
                    Instantiate(goblin, spawnerPos2.transform.position, Quaternion.identity);
                    Instantiate(goblin, spawnerPos3.transform.position, Quaternion.identity);
                }
            }
        }

        /// <summary>
        /// If player leaves the collider the trigger text dissapears
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            triggerText.gameObject.SetActive(false);
        }
    }
}