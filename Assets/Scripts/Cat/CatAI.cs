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

        Vector3 meleeAttackRange = new Vector3(0.4f, 0.8f, 0.4f);

        [Header("Movement")]
        [SerializeField]
        float movementSpeed = 10f;

        Tree tree = null;
        Animator animator = null;

        //float timePlayerWithinMeleeRange = 0f;

        private int randomAction;
        public Image canvasImage;
        private float fadeDuration = 2.0f;

        BoxCollider attackRangeCollider;
        Transform player;

        private void Awake()
        {
            attackRangeCollider = transform.parent.GetChild(0).GetComponent<BoxCollider>();
            animator = transform.parent.GetComponent<Animator>();
            tree = new Tree(SetTree());

            attackRangeCollider.gameObject.AddComponent<TriggerEvents>();


            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Player");
            if (taggedObjects.Length > 0)
            {
                player = taggedObjects[0].transform;
            }
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

            var moveToPlayer = new ActionNode(MoveToPlayer);

            var moveToOrigin = new ActionNode(MoveToOriginPosition);

            return new Selector(new List<Node>()
            { 
                meleeAttack,
                moveToPlayer,
                moveToOrigin
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
            if (player != null)
            {
                if (Vector3.SqrMagnitude(player.position - transform.parent.position) < meleeAttackRange.sqrMagnitude)
                {
                    /*timePlayerWithinMeleeRange += Time.deltaTime;

                    if (timePlayerWithinMeleeRange > 1.5f)
                    {
                        timePlayerWithinMeleeRange = 0f;*/

                        attackRangeCollider.enabled = true;
                        return Node.NodeState.SUCCESS;
                    //}

                }
            }
            attackRangeCollider.enabled = false;
            return Node.NodeState.FAILURE;  
        }

        Node.NodeState DoMeleeAttack()
        {
            if(player != null)
            {
                animator.SetTrigger("attack");
                StartCoroutine(FadeOut());
                //if (randomAction == 2) // 암전
                {
                    
                }

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
            //Color darkColor = new Color(imageColor.r * 0.5f, imageColor.g * 0.5f, imageColor.b * 0.5f, imageColor.a);
            Color darkColor = new Color(0, 0, 0, 240);
            
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                float progress = timer / fadeDuration;
                
                imageColor.a = Mathf.Lerp(0, 0.8f, progress);

                canvasImage.color = imageColor;
                yield return null;
            }
        }

        // RUN
        Node.NodeState MoveToPlayer()
        {
            if (player != null)
            {
                // 몬스터가 플레이어를 향해 회전
                Vector3 playerDirection = player.position - transform.parent.position;
                Quaternion targetRotation = Quaternion.LookRotation(playerDirection);
                transform.parent.rotation = targetRotation;

                // 몬스터가 플레이어를 향해 이동
                transform.parent.position = Vector3.MoveTowards(transform.parent.position, player.position, Time.deltaTime * movementSpeed);
                animator.SetTrigger("run");

                return Node.NodeState.RUNNING;
            }

            return Node.NodeState.FAILURE;
        }

        // IDLE
        Node.NodeState MoveToOriginPosition()
        {
            if (player != null)
            {
                if (Vector3.SqrMagnitude(player.position - transform.parent.position) < meleeAttackRange.sqrMagnitude)
                {
                    animator.SetTrigger("idle");
                    return Node.NodeState.SUCCESS;
                }
                
            }
            return Node.NodeState.FAILURE;
        }

        private void OnDrawGizmos()
        {
            // 탐지 거리
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.parent.position, detectRange);
        }
    }

    public class TriggerEvents : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player Attack!!");
            }
        }

        private void OnTriggerExit(Collider other)
        {
        }
    }
}