using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class Hitbox : MonoBehaviour
    {

        public bool isInField = false;

        private GoblinAI1 goblin;

        /// <summary>
        /// Checks if goblin is within player's collider
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Goblin")
            {
                isInField = true;
                goblin = other.GetComponent<GoblinAI1>();
            }
        }

        /// <summary>
        /// Checks if goblin is within player's collider
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Goblin")
            {
                isInField = false;
                goblin = null;
            }
        }

        /// <summary>
        /// Attacking goblin
        /// </summary>
        /// <param name="damage"></param>
        public void Attack(float damage)
        {
            if (isInField)
            {
                goblin.TakeDamage(damage);
            }
        }
    }
}