using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    }
    private void OnTriggerStay(Collider other)
    {
        if (!isPickedUp)
        {
            triggerText.gameObject.SetActive(true);
        }
        triggerText.text = "Press E to pick up " + name;
        if (Input.GetKeyDown(KeyCode.E))
        {
            for (int i = 0; i < data.Hotbar.Length; i++)
            {
                if (data.Hotbar[i] == null)
                {
                    data.Hotbar[i] = pickupItem;
                    triggerText.gameObject.SetActive(false);
                    i = data.Hotbar.Length + 1;
                    isPickedUp = true;
                    Destroy(Parent);
                }
            }
            data.ReloadHotbar();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        triggerText.gameObject.SetActive(false);
    }
}
