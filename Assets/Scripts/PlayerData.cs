using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    public int questNumber;
    public float DialogueNumber;

    [Header("PlayerStats")]
    public float MaxHealth;
    public float curHealth;

    public ObjectData[] Hotbar;
    public ObjectData[] Inventory;
    public Image[] inventorySlots;
    public Image[] hotbarSlots;
    public Image[] BackgroundSlots;

    public int curEquipped = 0;

    public Color equippedColor;
    public Color normalColor;


    [Header("UIComponents")]
    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    [HideInInspector]
    public bool isDragged;
    public int moveToSlot;
    public ObjectData data;
    public int parentItem;
    public bool isInInventory;

    public GameObject DraggingSprite;

    public GameObject Canvas;

    private GameObject TempDragObj;

    private UIManager manager;


    public GameObject cameraObj;

    public Transform dropArea;

    public TextMeshProUGUI triggertext;

    private GameObject CurrentEquipped;
    public void Start()
    {
        healthSlider.maxValue = MaxHealth;
        curHealth = MaxHealth;
        healthSlider.value = curHealth;
        healthText.text = curHealth.ToString("F0") + "/" + MaxHealth.ToString("F0");
        manager = FindObjectOfType<UIManager>();
        EquipHotbar();
    }

    public void EquipHotbar()
    {
        for (int i = 0; i < BackgroundSlots.Length; i++)
        {
            if (i == curEquipped)
            {
                BackgroundSlots[i].color = equippedColor;

                if (Hotbar[i] != null)
                {
                    Destroy(CurrentEquipped);
                    CurrentEquipped = Instantiate(Hotbar[i].weaponPrefab, transform.position, transform.rotation);
                    CurrentEquipped.transform.SetParent(cameraObj.transform);
                    CurrentEquipped.transform.localPosition = Hotbar[i].weaponPrefab.transform.position;
                    CurrentEquipped.transform.localRotation = Hotbar[i].weaponPrefab.transform.rotation;
                }
                else
                {
                    Destroy(CurrentEquipped);
                }

            }
            else
            {
                BackgroundSlots[i].color = normalColor;
            }
        }
    }

    private void Update()
    {
        if (!manager.InventorySystem.activeSelf)
        {
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            {

                if (curEquipped - 1 < 0)
                {
                    curEquipped = Hotbar.Length - 1;
                }
                else
                {
                    curEquipped--;
                }
                EquipHotbar();
            }
            else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            {
                if (curEquipped + 1 > Hotbar.Length - 1)
                {
                    curEquipped = 0;
                }
                else
                {
                    curEquipped++;
                }
                EquipHotbar();
            }

        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Hotbar[curEquipped] != null)
            {
                Instantiate(Hotbar[curEquipped].groundItem, dropArea.position, dropArea.rotation);
                Hotbar[curEquipped] = null;
                ReloadHotbar();
                EquipHotbar();
            }
        }
        if (TempDragObj != null)
        {
            TempDragObj.transform.position = Vector2.Lerp(TempDragObj.transform.position, Input.mousePosition, 0.3f);
        }
    }

    public void TakeDamage(float Damage)
    {
        curHealth -= Damage;
        healthSlider.value = curHealth;
        if (curHealth <= 0)
        {
            Application.LoadLevel("AdventureWorld");
        }
        healthText.text = curHealth.ToString("F0") + "/" + MaxHealth.ToString("F0");
    }

    public void ReloadHotbar()
    {
        for (int i = 0; i < Hotbar.Length; i++)
        {
            if (Hotbar[i] != null)
            {
                hotbarSlots[i].sprite = Hotbar[i].sprite;
                hotbarSlots[i].color = new Color(255, 255, 255, 255);
            }
            else
            {
                hotbarSlots[i].sprite = null;
                hotbarSlots[i].color = new Color(0, 0, 0, 0);
            }
        }
    }

    public void ReloadInventory()
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] != null)
            {
                inventorySlots[i].sprite = Inventory[i].sprite;
                inventorySlots[i].color = new Color(255, 255, 255, 255);
            }
            else
            {
                inventorySlots[i].sprite = null;
                inventorySlots[i].color = new Color(0, 0, 0, 0);
            }
        }
    }

    public void Heal(float Health)
    {
        curHealth += Health;

        if (curHealth > MaxHealth)
            curHealth = MaxHealth;

        healthSlider.value = curHealth;
        healthText.text = curHealth.ToString("F0") + "/" + MaxHealth.ToString("F0");
    }

    public void DragItem(int indexNum, ObjectData passingObj, bool isInventory)
    {
        if (!isDragged)
        {
            if (passingObj != null)
            {
                if (!isInventory)
                {
                    isDragged = true;
                    moveToSlot = indexNum;
                    Hotbar[indexNum] = null;
                    data = passingObj;
                    ReloadHotbar();
                    hotbarSlots[indexNum].gameObject.GetComponent<Slot>().Objdata = null;

                    TempDragObj = Instantiate(DraggingSprite, DraggingSprite.transform.position, DraggingSprite.transform.rotation);
                    TempDragObj.transform.SetParent(Canvas.transform);
                    TempDragObj.GetComponent<Image>().sprite = data.sprite;
                    isInventory = false;
                }
                else
                {
                    isDragged = true;
                    moveToSlot = indexNum;
                    Inventory[indexNum] = null;
                    data = passingObj;
                    ReloadInventory();
                    inventorySlots[indexNum].gameObject.GetComponent<Slot>().Objdata = null;

                    TempDragObj = Instantiate(DraggingSprite, DraggingSprite.transform.position, DraggingSprite.transform.rotation);
                    TempDragObj.transform.SetParent(Canvas.transform);
                    TempDragObj.GetComponent<Image>().sprite = data.sprite;
                    isInventory = true;
                }
            }
        }
        else
        {
            if (!isInventory)
            {
                isDragged = false;
                moveToSlot = -1;
                Hotbar[indexNum] = data;
                hotbarSlots[indexNum].gameObject.GetComponent<Slot>().Objdata = data;
                data = null;
                ReloadHotbar();
                Destroy(TempDragObj);
                TempDragObj = null;
            }
            else
            {
                isDragged = false;
                moveToSlot = -1;
                Inventory[indexNum] = data;
                inventorySlots[indexNum].gameObject.GetComponent<Slot>().Objdata = data;
                data = null;
                ReloadInventory();
                Destroy(TempDragObj);
                TempDragObj = null;
            }
        }
    }



}