using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class GroundItem : MonoBehaviour
    {
        public TextMeshProUGUI triggerText;
        public new string name;
        public ObjectData pickupItem;

        private PlayerData data;

        private bool isPickedUp;

        public GameObject Parent;

        private void Start()
        {
            data = FindObjectOfType<PlayerData>();
            triggerText = data.triggertext;
        }

        /// <summary>
        /// If player is near an item, it will prompt a trigger text that allows the player to get the item
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerStay(Collider other)
        {
            if (other.tag == "Player")
            {
                if (!isPickedUp)
                {
                    triggerText.gameObject.SetActive(true);
                }
                triggerText.text = "Press F to pick up " + name;
                if (Input.GetKeyDown(KeyCode.F))
                {
                    for (int i = 0; i < data.Hotbar.Length; i++)
                    {
                        if (data.Hotbar[i] == null)
                        {
                            data.Hotbar[i] = pickupItem;
                            data.hotbarSlots[i].gameObject.GetComponent<Slot>().Objdata = pickupItem;
                            triggerText.gameObject.SetActive(false);
                            i = data.Hotbar.Length + 1;
                            isPickedUp = true;
                            Destroy(Parent);
                        }
                    }
                    data.ReloadHotbar();
                }
            }
        }


        /// <summary>
        /// If player leaves the collider it will not prompt the text trigger anymore
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            triggerText.gameObject.SetActive(false);
        }
    }
}
