using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace UnityStandardAssets.Characters.FirstPerson
{
    public class GoblinBerserkerAI : MonoBehaviour
    {
        private NavMeshAgent nav;
        public GameObject player;
        public Animator anim;
        public ParticleSystem blood;

        private PlayerData data;

        public float minDamage;
        public float maxDamage;
        private UIManager manager;


        public Slider healthSlider;

        public float maxHealth;
        public float health;

        private bool isAttacking = false;

        private float updateTime = 0;
        public void Start()
        {
            nav = GetComponent<NavMeshAgent>();
            data = FindObjectOfType<PlayerData>();
            healthSlider.maxValue = maxHealth;
            health = maxHealth;
            healthSlider.value = health;
            player = GameObject.FindWithTag("Player");
            manager = FindObjectOfType<UIManager>();
        }

        /// <summary>
        /// If certain distance the goblin will attack.
        /// </summary>
        public void Update()
        {
            updateTime += Time.deltaTime;

            float dist = Vector3.Distance(this.transform.position, player.transform.position);
            if (dist <= 4)
            {
                if (!isAttacking)
                {
                    StartCoroutine(Attack());
                }
            }
            else
            {
                anim.SetBool("Attack", false);
                isAttacking = false;
            }

            if (health <= 0)
            {
                StartCoroutine(Die());
            }
        }

        /// <summary>
        /// Animation for defeating the goblin champion and it shows the victory screen after a few seconds.
        /// </summary>
        /// <returns></returns>
        IEnumerator Die()
        {
            anim.SetBool("Attack", false);
            anim.SetBool("Die", true);
            yield return new WaitForSeconds(3f);
            manager.VictoryScreen();
        }

        /// <summary>
        /// Goblin taking damage and blood particle.
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(float damage)
        {
            if (health > 0)
            {
                blood.Play();
            }
            health -= damage;
            healthSlider.value = health;

        }

        /// <summary>
        /// Attacking animation and calculation of damage.
        /// </summary>
        /// <returns></returns>
        IEnumerator Attack()
        {
            if (!isAttacking)
            {
                isAttacking = true;
                anim.SetBool("Attack", true);
                yield return new WaitForSeconds(1.2f);
                data.TakeDamage(Random.Range(minDamage, maxDamage));
                isAttacking = false;
            }
        }

        /// <summary>
        /// Goblin will look at the player.
        /// </summary>
        private void LateUpdate()
        {
            transform.LookAt(player.transform);
            if (updateTime > 2)
            {
                nav.destination = player.transform.position;
                updateTime = 0;
            }
        }

    }
}