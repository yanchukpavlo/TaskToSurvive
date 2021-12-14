using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] int hp = 100;
    [SerializeField] int damage = 10;
    [SerializeField] Transform attackTr;
    [SerializeField] float attackRate = 2f;
    [SerializeField] float attackRadius = 1f;
    [SerializeField] LayerMask attackLayer;
    [SerializeField] GameObject playerWall;

    bool alive = true;
    bool playerAlive = true;
    int currentHP;
    float attackTimer;
    Transform target;
    NavMeshAgent agent;
    Animator animator;

    static int animSpeed = Animator.StringToHash("Speed");
    static int animAttack = Animator.StringToHash("Attack");

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        target = Player.Instance.transform;
        EventsManager.Instance.onChangeStateTrigger += ChangeStateTrigger;
    }

    private void OnDestroy()
    {
        EventsManager.Instance.onChangeStateTrigger -= ChangeStateTrigger;
    }

    private void OnEnable()
    {
        alive = true;
        animator.enabled = true;
        agent.enabled = true;
        currentHP = hp;
        playerWall.SetActive(true);
        StartCoroutine(UpdateTarget());
    }

    private void Update()
    {
        if (alive)
        {
            animator.SetFloat(animSpeed, agent.velocity.magnitude);
        }
    }

    private void ChangeStateTrigger(GameState state)
    {
        switch (state)
        {
            case GameState.Lose:
                playerAlive = false;
                break;
        }
    }

    public void TakeDamege(int damage)
    {
        if (alive)
        {
            currentHP -= damage;
            if (currentHP <= 0)
            {
                alive = false;
                DoDestroy();
            }
        }
    }

    public void DoDestroy()
    {
        animator.enabled = false;
        agent.enabled = false;
        playerWall.SetActive(false);
        EventsManager.Instance.EnemyDieTrigger();
    }

    void Attack()
    {
        if (attackTimer < Time.time)
        {
            if (Physics.CheckSphere(attackTr.position, attackRadius, attackLayer))
            {
                Player.Instance.TakeDamege(damage);
                attackTimer = Time.time + attackRate;
                animator.SetTrigger(animAttack);
            }
        }
    }

    IEnumerator UpdateTarget()
    {
        yield return new WaitForSeconds(0.1f);
        if (alive)
        {
            agent.SetDestination(target.position);
            if ((target.position - transform.position).magnitude < agent.stoppingDistance + 0.1f)
            {
                Attack();
            }

            yield return new WaitForSeconds(0.1f);
            if (playerAlive) StartCoroutine(UpdateTarget());
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(attackTr.position, attackRadius);
    }
}
