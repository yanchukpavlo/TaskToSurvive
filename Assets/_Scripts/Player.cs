using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    public static Player Instance { get; private set; }


    [SerializeField] int hp = 100;

    bool alive = true;
    int currentHP;
    Animator animator;

    public bool Alive { get { return alive; } }

    static int AnimHit = Animator.StringToHash("Hit");

    private void Awake()
    {
        Instance = this;
        currentHP = hp;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        UIManager.Instance.SetupSliderPlayerHP(currentHP);
    }

    public void TakeDamege(int damage)
    {
        if (alive)
        {
            currentHP -= damage;
            UIManager.Instance.SetSliderPlayerHP(currentHP);
            if (currentHP <= 0)
            {
                alive = false;
                DoDestroy();
            }

            animator.SetTrigger(AnimHit);
        }
    }

    public void DoDestroy()
    {
        EventsManager.Instance.ChangeStateTrigger(GameState.Lose);
        animator.enabled = false;
        UIManager.Instance.SetShowText("You lose :(");
        GetComponent<ThirdPersonShooterController>().enabled = false;
        GetComponent<StarterAssets.ThirdPersonController>().CanMove = false;
    }
}
