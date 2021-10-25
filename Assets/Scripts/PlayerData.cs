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
    public Image[] hotbarSlots;


    [Header("UIComponents")]
    public Slider healthSlider;
    public TextMeshProUGUI healthText;

    public void Start()
    {
        healthSlider.maxValue = MaxHealth;
        curHealth = MaxHealth;
        healthSlider.value = curHealth;
        healthText.text = curHealth.ToString("F0") + "/" + MaxHealth.ToString("F0");
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
}
