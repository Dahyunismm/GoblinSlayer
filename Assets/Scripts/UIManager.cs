using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject DialogueObj;
    public GameObject HotBarObj;
    public GameObject InventorySystem;

    private PlayerData data;

    private void Start()
    {
        data = FindObjectOfType<PlayerData>();
    }
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

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InventorySystem.SetActive(!InventorySystem.activeSelf);

            if (InventorySystem.activeSelf)
            {
                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            } else
            {
                Time.timeScale = 1;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                if(data.isDragged)
                {
                    data.DragItem(data.moveToSlot, data.data, data.isInInventory);
                }
            }
            
        }
    }
}
