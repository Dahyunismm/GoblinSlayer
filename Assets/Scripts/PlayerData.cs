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
    public float maxSaturation;
    public float currentSaturation;
    public float ArmorStats;
    public float StrengthStats;
    public float CharismaStats;
    public float AgilityStats;
    public float EnduranceStats;
    public float intelligenceStats;

    public ObjectData[] Hotbar;
    public ObjectData[] EquipmentTab;
    public ObjectData[] Inventory;
    public Image[] inventorySlots;
    public Image[] hotbarSlots;
    public Image[] equipmentSlots;
    public Image[] BackgroundSlots;

    public int curEquipped = 0;

    public Color equippedColor;
    public Color normalColor;

    [Header("StatsText")]
    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI ArmorText;
    public TextMeshProUGUI SaturationText;
    public TextMeshProUGUI StrengthText;
    public TextMeshProUGUI CharismaText;
    public TextMeshProUGUI AgilityText;
    public TextMeshProUGUI EnduranceText;
    public TextMeshProUGUI intelligenceText;

    [Header("UIComponents")]
    public Slider healthSlider;
    public TextMeshProUGUI healthText;
    public Slider saturationSlider;
    public TextMeshProUGUI saturationText;

    [HideInInspector]
    public bool isDragged;
    public int moveToSlot;
    public ObjectData data;
    public int parentItem;
    public bool isInInventory;
    public bool isequipment;

    public GameObject DraggingSprite;

    public GameObject Canvas;

    private GameObject TempDragObj;

    private UIManager manager;

    public Slider eatSlider;


    public GameObject cameraObj;

    public Transform dropArea;

    public TextMeshProUGUI triggertext;

    private GameObject CurrentEquipped;

    private float saturationTimer = 0;
    public void Start()
    {
        healthSlider.maxValue = MaxHealth;
        saturationSlider.maxValue = maxSaturation;
        currentSaturation = maxSaturation;
        curHealth = MaxHealth;
        healthSlider.value = curHealth;
        saturationSlider.value = currentSaturation;
        healthText.text = curHealth.ToString("F0") + "/" + MaxHealth.ToString("F0");
        saturationText.text = currentSaturation.ToString("F0") + "/" + maxSaturation.ToString("F0");
        manager = FindObjectOfType<UIManager>();
        EquipHotbar();
    }

    public void UpdateStats()
    {
        HealthText.text = "Health: " + curHealth.ToString("F0") + "/" + MaxHealth.ToString("F0");
        saturationText.text = currentSaturation.ToString("F0") + "/" + maxSaturation.ToString("F0");
        ArmorText.text = "Armor: " + ArmorStats.ToString("F0");
        StrengthText.text = "Strength: " + StrengthStats.ToString("F0");
        CharismaText.text = "Charisma: " + CharismaStats.ToString("F0");
        AgilityText.text = "Agility: " + AgilityStats.ToString("F0");
        EnduranceText.text = "Endurance: " + EnduranceStats.ToString("F0");
        intelligenceText.text = "Intelligence: " + intelligenceStats.ToString("F0");
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
        if (saturationTimer < 15)
        {
            saturationTimer += Time.deltaTime;
        }
        else
        {
            currentSaturation -= Random.Range(1, 3);
            saturationText.text = currentSaturation.ToString("F0") + "/" + maxSaturation.ToString("F0");
            saturationSlider.value = currentSaturation;
            saturationTimer = 0;

        }
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

    public void lateUpdatehotbar(float time)
    {
        Invoke("ReloadHotbar", time);
    }

    public void TakeDamage(float Damage)
    {
        curHealth -= Damage;
        healthSlider.value = curHealth;
        if (curHealth <= 0)
        {
            //manager.deathScreen();
        }
        healthText.text = curHealth.ToString("F0") + "/" + MaxHealth.ToString("F0");
    }

    public void Respawn()
    {
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Application.LoadLevel("StartingVillage");
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
    public void ReloadEquipment()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (EquipmentTab[i] != null)
            {
                equipmentSlots[i].sprite = EquipmentTab[i].sprite;
                equipmentSlots[i].color = new Color(255, 255, 255, 255);
            }
            else
            {
                equipmentSlots[i].sprite = null;
                equipmentSlots[i].color = new Color(0, 0, 0, 0);
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

    public void DragItem(int indexNum, ObjectData passingObj, bool isInventory, bool isEquipment)
    {
        if (!isDragged)
        {
            if (passingObj != null)
            {
                if (!isInventory && !isEquipment)
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
                else if (!isInventory && isEquipment)
                {
                    isDragged = true;
                    moveToSlot = indexNum;
                    EquipmentTab[indexNum] = null;
                    data = passingObj;
                    ReloadEquipment();
                    equipmentSlots[indexNum].gameObject.GetComponent<Slot>().Objdata = null;
                    ArmorStats -= data.armorValue;
                    UpdateStats();
                    TempDragObj = Instantiate(DraggingSprite, DraggingSprite.transform.position, DraggingSprite.transform.rotation);
                    TempDragObj.transform.SetParent(Canvas.transform);
                    TempDragObj.GetComponent<Image>().sprite = data.sprite;
                    isequipment = true;
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
            if (!isInventory && !isEquipment)
            {
                if (hotbarSlots[indexNum].gameObject.GetComponent<Slot>().Objdata == null)
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
            }
            else if (!isInventory && isEquipment)
            {
                if (data.itemType == "chestplate" && indexNum == 1)
                {
                    isDragged = false;
                    moveToSlot = -1;
                    EquipmentTab[indexNum] = data;
                    equipmentSlots[indexNum].gameObject.GetComponent<Slot>().Objdata = data;
                    ArmorStats += data.armorValue;
                    data = null;
                    ReloadEquipment();
                    Destroy(TempDragObj);
                    TempDragObj = null;
                    UpdateStats();
                }
                else if (data.itemType == "helmet" && indexNum == 0)
                {
                    isDragged = false;
                    moveToSlot = -1;
                    EquipmentTab[indexNum] = data;
                    equipmentSlots[indexNum].gameObject.GetComponent<Slot>().Objdata = data;
                    ArmorStats += data.armorValue;
                    data = null;
                    ReloadEquipment();
                    Destroy(TempDragObj);
                    UpdateStats();
                    TempDragObj = null;
                }
                else if (data.itemType == "boots" && indexNum == 2)
                {
                    isDragged = false;
                    moveToSlot = -1;
                    EquipmentTab[indexNum] = data;
                    equipmentSlots[indexNum].gameObject.GetComponent<Slot>().Objdata = data;
                    ArmorStats += data.armorValue;
                    data = null;
                    ReloadEquipment();
                    Destroy(TempDragObj);
                    UpdateStats();
                    TempDragObj = null;
                }
                else if (data.itemType == "leggings" && indexNum == 4)

                {
                    isDragged = false;
                    moveToSlot = -1;
                    EquipmentTab[indexNum] = data;
                    equipmentSlots[indexNum].gameObject.GetComponent<Slot>().Objdata = data;
                    ArmorStats += data.armorValue;
                    data = null;
                    ReloadEquipment();
                    Destroy(TempDragObj);
                    UpdateStats();
                    TempDragObj = null;
                }

            }
            else
            {
                if (inventorySlots[indexNum].gameObject.GetComponent<Slot>().Objdata == null)
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



}