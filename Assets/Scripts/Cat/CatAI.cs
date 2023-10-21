using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    [RequireComponent(typeof(Animator))]
    public class CatAI : MonoBehaviour
    {
        [Header("Range")]
        [SerializeField]
        float detectRange = 10f;

        Vector3 meleeAttackRange = new Vector3(1f, 2f, 1f);

        [Header("Movement")]
        [SerializeField]
        float movementSpeed = 10f;

        Tree tree = null;
        Transform detectedPlayer = null;
        Animator animator = null;

        const string attackStateName = "Attack";
        const string attackTriggerName = "attack";

        private void Awake()
        {
            animator = GetComponent<Animator>();
            tree = new Tree(SetTree());
        }

        private void Update()
        {
            tree.Operate();
        }

        Node SetTree()
        {
            var meleeAttack = new Sequence(
                new List<Node>()
                {
                    new ActionNode(CheckMeleeAttack),
                    new ActionNode(CheckPlayerWithinMeleeAttackRange),
                    new ActionNode(DoMeleeAttack),
                });

            var detectPlayer = new Sequence(
                new List<Node>()
                {
                    new ActionNode(CheckDetectPlayer),
                    new ActionNode(MoveToDetectPlayer),
                });

            //var moveToOrigin = new ActionNode(MoveToOriginPosition);

            return new Selector(new List<Node>()
            {
                meleeAttack,
                detectPlayer,
                //moveToOrigin
            });
        }

        bool IsAnimationRunning(string stateName)
        {
            if(animator != null)
            {
                if(animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
                {
                    var normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

                    return normalizedTime != 0 && normalizedTime < 1f;
                }
            }
            return false;
        }

        Node.NodeState CheckMeleeAttack()
        {
            if (IsAnimationRunning(attackStateName)) return Node.NodeState.RUNNING;
            return Node.NodeState.SUCCESS;
        }

        Node.NodeState CheckPlayerWithinMeleeAttackRange()
        {
            if (detectedPlayer != null)
            {
                if (Vector3.SqrMagnitude(detectedPlayer.position - transform.position) < meleeAttackRange.sqrMagnitude) return Node.NodeState.SUCCESS;
            }
            return Node.NodeState.FAILURE;  
        }

        Node.NodeState DoMeleeAttack()
        {
            if(detectedPlayer != null)
            {
                animator.SetTrigger(attackTriggerName);
                return Node.NodeState.SUCCESS;
            }
            return Node.NodeState.FAILURE;
        }

        Node.NodeState CheckDetectPlayer()
        {
            var overlapColliders = Physics.OverlapSphere(transform.position, detectRange, LayerMask.GetMask("Player"));
            
            if(overlapColliders != null && overlapColliders.Length >0)
            {
                detectedPlayer = overlapColliders[0].transform;

                return Node.NodeState.SUCCESS;
            }

            detectedPlayer = null;

            return Node.NodeState.FAILURE;
        }

        Node.NodeState MoveToDetectPlayer()
        {
            if(detectedPlayer != null)
            {
                if(Vector3.SqrMagnitude(detectedPlayer.position - transform.position) < meleeAttackRange.sqrMagnitude)
                {
                    return Node.NodeState.SUCCESS;
                }
            }

            animator.SetTrigger("walk");
            transform.position = Vector3.MoveTowards(transform.position, detectedPlayer.position, Time.deltaTime * movementSpeed); ;
            return Node.NodeState.RUNNING;
        }

        private void OnDrawGizmos()
        {
            // 탐지 거리
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, detectRange);

            // 근접 공격 사거리
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(this.transform.position + new Vector3(2f, 1f, 0f), meleeAttackRange);
        }
    }
}
