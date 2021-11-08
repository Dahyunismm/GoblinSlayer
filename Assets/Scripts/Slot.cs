using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Slot : MonoBehaviour, IPointerClickHandler
    {
        public int slotNumber;

        public ObjectData Objdata;

        private PlayerData data;

        public bool isInventory = true;

        public bool isEquipment = false;

        void Awake()
        {
            data = FindObjectOfType<PlayerData>();
        }

        /// <summary>
        /// Pointing with cursor and clicking on item allows the player to drag it.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            data.DragItem(slotNumber, Objdata, isInventory, isEquipment);
        }

    }
}