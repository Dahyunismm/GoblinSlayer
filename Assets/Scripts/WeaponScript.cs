using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public ObjectData data;

    public Animator anim;

    public bool isAttacking = false;

    private Hitbox hitbox;

    private void Start()
    {
        hitbox = FindObjectOfType<Hitbox>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(Attack());
        }
    }

    // Attacking and Attack Cooldown to avoid spamming 
    IEnumerator Attack()
    {
        if (!isAttacking)
        {
            hitbox.Attack(data.AttackDamage);
            isAttacking = true;
            anim.SetBool("isAttacking", true);
            yield return new WaitForSeconds(data.AttackSpeed);
            anim.SetBool("isAttacking", false);
            isAttacking = false;
        }
    }
}