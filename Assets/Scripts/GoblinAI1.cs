using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoblinAI1 : MonoBehaviour
{
    private NavMeshAgent nav;
    public GameObject player;
    public Animator anim;

    private PlayerData data;

    public float minDamage;
    public float maxDamage;

    private bool isAttacking = false;

    private float updateTime = 0;
    public void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        data = FindObjectOfType<PlayerData>();
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
