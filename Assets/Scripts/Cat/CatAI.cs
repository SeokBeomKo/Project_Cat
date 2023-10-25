using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTree
{
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

        //float timePlayerWithinMeleeRange = 0f;

        private int randomAction;
        private Image canvasImage;
        private float fadeDuration = 2.0f;

        private void Awake()
        {
            animator = transform.parent.GetComponent<Animator>();
            tree = new Tree(SetTree());

            canvasImage = GetComponentInChildren<Image>();
        }

        private void Update()
        {
            randomAction = Random.Range(1, 3);
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

            var moveToOrigin = new ActionNode(MoveToOriginPosition);

            return new Selector(new List<Node>()
            { 
                meleeAttack,
                moveToOrigin,
                detectPlayer
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

        // 근접 기본 공격
        Node.NodeState CheckMeleeAttack()
        {
            if (IsAnimationRunning("Attack")) return Node.NodeState.RUNNING;
            return Node.NodeState.SUCCESS;
        }

        Node.NodeState CheckPlayerWithinMeleeAttackRange()
        {
            if (detectedPlayer != null)
            {
                if (Vector3.SqrMagnitude(detectedPlayer.position - transform.parent.position) < meleeAttackRange.sqrMagnitude)
                {
                    /*timePlayerWithinMeleeRange += Time.deltaTime;

                    if (timePlayerWithinMeleeRange > 1.5f)
                    {
                        timePlayerWithinMeleeRange = 0f;*/
                        return Node.NodeState.SUCCESS;
                    //}

                }
            }
            return Node.NodeState.FAILURE;  
        }

        Node.NodeState DoMeleeAttack()
        {
            if(detectedPlayer != null)
            {
                animator.SetTrigger("attack");

                //if (randomAction == 2) // 암전
                //{
                    StartCoroutine(FadeOut());
                //}

                return Node.NodeState.SUCCESS;
            }
            return Node.NodeState.FAILURE;
        }

        IEnumerator FadeOut()
        {
            /*float timer = 0f;
            Color imageColor = canvasImage.color;
            Color targetColor = new Color(imageColor.r, imageColor.g, imageColor.b, 0f); // 알파값을 0으로 만듭니다.

            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                float progress = timer / fadeDuration;
                canvasImage.color = Color.Lerp(imageColor, targetColor, progress);
                yield return null;
            }*/
            float timer = 0f;
            Color imageColor = canvasImage.color;

            // 어두운 색상 (0.5의 값은 임의로 설정할 수 있습니다)
            Color darkColor = new Color(imageColor.r * 0.5f, imageColor.g * 0.5f, imageColor.b * 0.5f, imageColor.a);

            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                float progress = timer / fadeDuration;
                canvasImage.color = Color.Lerp(imageColor, darkColor, progress);
                yield return null;
            }
        }

        // IDLE
        Node.NodeState MoveToOriginPosition()
        {
            if (detectedPlayer != null)
            {
                if (Vector3.SqrMagnitude(detectedPlayer.position - transform.parent.position) < meleeAttackRange.sqrMagnitude)
                {
                    animator.SetTrigger("idle");
                    return Node.NodeState.SUCCESS;
                }
                
            }
            return Node.NodeState.FAILURE;
        }

        // RUN
        Node.NodeState CheckDetectPlayer()
        {
            var overlapColliders = Physics.OverlapSphere(transform.parent.position, detectRange, LayerMask.GetMask("Player"));

            if (overlapColliders != null && overlapColliders.Length > 0)
            {
                detectedPlayer = overlapColliders[0].transform;

                return Node.NodeState.SUCCESS;
            }

            detectedPlayer = null;

            return Node.NodeState.FAILURE;
        }

        Node.NodeState MoveToDetectPlayer()
        {
            if (detectedPlayer != null)
            {
                if (Vector3.SqrMagnitude(detectedPlayer.position - transform.parent.position) < meleeAttackRange.sqrMagnitude)
                {

                    return Node.NodeState.SUCCESS;
                }
            }

            animator.SetTrigger("walk");
            transform.parent.position = Vector3.MoveTowards(transform.parent.position, detectedPlayer.position, Time.deltaTime * movementSpeed); ;
            return Node.NodeState.RUNNING;
        }

        private void OnDrawGizmos()
        {
            // 탐지 거리
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.parent.position, detectRange);

            // 근접 공격 사거리
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(this.transform.parent.position + new Vector3(2f, 1f, 0f), meleeAttackRange);
        }
    }
}
