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
                anim.SetBool("Attack", false);
                anim.SetBool("Die", true);
                Destroy(gameObject, 1f);
                manager.VictoryScreen();
            }
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            healthSlider.value = health;
        }

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