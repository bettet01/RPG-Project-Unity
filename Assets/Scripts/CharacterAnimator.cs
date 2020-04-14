using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    public AnimationClip replaceableAttackAnimation;
    // animation arrays
    protected AnimationClip[] currentAttackAnimationSet;
    public AnimationClip[] defaultAttackAnimationSet;


    public AnimatorOverrideController OverrideController;
    const float locationAnimationSmoothTime = .15f;
    protected Animator animator;
    NavMeshAgent agent;
    protected CharacterCombat combat;


    // Start is called before the first frame update
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        combat = GetComponent<CharacterCombat>();
        if(OverrideController == null)
        {
            OverrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
        }
        animator.runtimeAnimatorController = OverrideController;
        currentAttackAnimationSet = defaultAttackAnimationSet;

        combat.OnAttack += OnAttack;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        float speedPercent = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("SpeedPercent", speedPercent, locationAnimationSmoothTime, Time.deltaTime);
        animator.SetBool("inCombat", combat.inCombat);
    }

    protected virtual void OnAttack()
    {
        animator.SetTrigger("Attack");
        int attackIndex = Random.Range(0, currentAttackAnimationSet.Length);
        OverrideController[replaceableAttackAnimation.name] = currentAttackAnimationSet[attackIndex];
    }


}
