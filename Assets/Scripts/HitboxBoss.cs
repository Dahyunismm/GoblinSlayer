using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class HitboxBoss : MonoBehaviour
    {

        public bool isInField = false;

        private GoblinBerserkerAI goblin;

        /// <summary>
        /// Checks if goblin is within player's collider
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "GoblinBoss")
            {
                isInField = true;
                goblin = other.GetComponent<GoblinBerserkerAI>();
            }
        }

        /// <summary>
        /// Checks if goblin is within player's collider
        /// </summary>
        /// <param name="other"></param>
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "GoblinBoss")
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