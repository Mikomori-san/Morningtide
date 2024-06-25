using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class EnemyPatrolling : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private GameObject patrolPoint;
    [SerializeField] private GameObject patrolPoint2;
    [SerializeField] private GameObject player;
    private GameObject currentTarget;
    private bool isChasingPlayer = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
        currentTarget = patrolPoint;
        StartCoroutine(Patrol());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            isChasingPlayer = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            isChasingPlayer = false;
            currentTarget = patrolPoint;
        }
    }

    IEnumerator Patrol()
    {
        _animator.SetBool("isRunning", true);
        while (true)
        {
            if (isChasingPlayer)
            {
                currentTarget = player;
            }

            Vector3 targetPosition = new Vector3(currentTarget.transform.position.x, transform.position.y, currentTarget.transform.position.z);
            Vector3 direction = (targetPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 10);

            if (!isChasingPlayer && Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                _animator.SetBool("isRunning", false);
                yield return new WaitForSeconds(2f);
                currentTarget = currentTarget == patrolPoint ? patrolPoint2 : patrolPoint;
                _animator.SetBool("isRunning", true);
            }

            yield return null;
        }
    }
}
