using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    public float lookRadius = 10f;
	public Transform player;


	private CharacterStats EnemyStats;
	private GameObject victim;

	private CharacterStats HeroStats;
    NavMeshAgent agent;
	Animator m_Animator;


	// Start is called before the first frame update
	void Start()
    {
		m_Animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
	}

    // Update is called once per frame
    void Update()
    {
		float distance = Vector3.Distance(player.position, transform.position);

		if (distance <= agent.stoppingDistance)
		{
			//Animation Controller Code
            bool isWalking = false;
			m_Animator.SetBool("isWalking", isWalking);
			bool isStabbing = true;
			m_Animator.SetBool("isStabbing", isStabbing);
            //Gets Monster Stats to help calculate damage
			EnemyStats = gameObject.GetComponent<CharacterStats>();
			//Get Hero Stats to deal damage
			victim = GameObject.Find("Heroguy");
			HeroStats = victim.GetComponent<CharacterStats>();
			//Attack Target Code
			HeroStats.TakeDamage(15+EnemyStats.strength.GetMod());

			//Face the target
			FaceTarget();

		}
		else if (distance <= lookRadius)
        {
			bool isWalking = true;
			m_Animator.SetBool("isWalking", isWalking);
			bool isStabbing = false;
			m_Animator.SetBool("isStabbing", isStabbing);

			agent.SetDestination(player.position);

			//Face the target
			FaceTarget();
		}

	}

	void FaceTarget()
	{
		Vector3 direction = (player.position - transform.position);
		Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 1f);
	}


	void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
