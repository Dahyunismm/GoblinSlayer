using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEqupmentChanged += OnEqupmentChanged;
    }

    // Update is called once per frame
    void OnEqupmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damageModifier);
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(newItem.armorModifier);
            damage.RemoveModifier(newItem.armorModifier);
        }
        
    }
}