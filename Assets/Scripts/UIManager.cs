using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class UIManager : MonoBehaviour
    {
        public GameObject DialogueObj;
        public GameObject HotBarObj;
        public GameObject InventorySystem;
        public GameObject StatsPanel;
        public GameObject uiEmpty;
        public GameObject deathscreenobj;
        public GameObject victorycreenobj;
        public GameObject escapeScreen;

        private RigidbodyFirstPersonController rigid;
        private PlayerData data;
        public GameObject player;

        private void Start()
        {
            rigid = FindObjectOfType<RigidbodyFirstPersonController>();
            data = FindObjectOfType<PlayerData>();
        }


        private void Update()
        {
            // Makes sure that the hotbar is closed when in a dialogue
            if (DialogueObj.activeSelf)
            {
                HotBarObj.SetActive(false);
            }
            else
            {
                HotBarObj.SetActive(true);
            }

            // If the tab key is down, it pauses the game and makes the cursor is visible and if pressed again the game resumes and hides the cursor.
            if (Input.GetKeyDown(KeyCode.Tab) && !escapeScreen.activeSelf && !deathscreenobj.activeSelf && !victorycreenobj.activeSelf)
            {
                uiEmpty.SetActive(!uiEmpty.activeSelf);
                data.UpdateStats();
                if (uiEmpty.activeSelf)
                {
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Time.timeScale = 1;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                    if (data.isDragged)
                    {
                        data.DragItem(data.moveToSlot, data.data, data.isInInventory, data.isequipment);
                    }
                }

            }

            // Shows the Escape Screen
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EscapeScreen();
            }
        }

        /// <summary>
        /// Clicking on stats button will prompt the stats panel
        /// </summary>
        public void ClickOnStats()
        {
            StatsPanel.SetActive(true);
            InventorySystem.SetActive(false);
        }

        /// <summary>
        /// Clicking on inventory button will prompt the stats panel
        /// </summary>
        public void ClickOnInventory()
        {
            StatsPanel.SetActive(false);
            InventorySystem.SetActive(true);
        }

        public void EscapeScreen()
        {
            if (!deathscreenobj.activeSelf && !victorycreenobj.activeSelf)
            {
                escapeScreen.SetActive(!escapeScreen.activeSelf);
                if (escapeScreen.activeSelf)
                {
                    Time.timeScale = 0;
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    Time.timeScale = 1;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }
        }

        // Shows Death Screen, then freezes the time and enables the cursor.
        public void DeathScreen()
        {
            deathscreenobj.SetActive(true);
            rigid.enabled = false;
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        // Shows Victory Screen, then freezes the time and enables the cursor.
        public void VictoryScreen()
        {
            victorycreenobj.SetActive(true);
            rigid.enabled = false;
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
