using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

    public void OnPointerClick(PointerEventData eventData)
    {
        data.DragItem(slotNumber, Objdata, isInventory, isEquipment);
    }

}