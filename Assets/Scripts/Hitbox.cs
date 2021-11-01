using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{

    public bool isInField = false;

    private GoblinAI1 goblin;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Goblin")
        {
            isInField = true;
            goblin = other.GetComponent<GoblinAI1>();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Goblin")
        {
            isInField = false;
            goblin = null;
        }
    }
    public void Attack(float damage)
    {
        if (isInField)
        {
            goblin.TakeDamage(damage);
        }
    }
}