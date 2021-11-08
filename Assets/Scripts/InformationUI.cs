using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class InformationUI : MonoBehaviour
    {
        public TextMeshProUGUI triggerText;
        public GameObject GameInfo;
        private PlayerData data;

        private void Start()
        {
            data = FindObjectOfType<PlayerData>();
            triggerText = data.triggertext;
        }

        /// <summary>
        /// If player enters the collider prompts a message.
        /// </summary>
        /// <param name="other">the collider</param>
        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Player")
            {
                triggerText.gameObject.SetActive(true);
                GameInfo.gameObject.SetActive(true);
                triggerText.text = "Press X to begin your adventure!";
                if (Input.GetKeyDown(KeyCode.X))
                {
                    SceneManager.LoadScene("AdventureWorld");
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
            GameInfo.gameObject.SetActive(false);

        }
    }
}
