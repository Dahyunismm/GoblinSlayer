using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UnityStandardAssets.Characters.FirstPerson
{
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

        /// <summary>
        /// Attacking when left click is pressed
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                StartCoroutine(Attack());
            }
        }

        /// <summary>
        /// Attacking and Attack Cooldown to avoid spamming
        /// </summary>
        /// <returns></returns>    
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
}