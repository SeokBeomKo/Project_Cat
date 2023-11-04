using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTree
{
    public class CatAI : MonoBehaviour
    {
        [Header("DarkImage")]
        public Image canvasImage;

        [Header("Movement")]
        [SerializeField]
        private float movementSpeed = 10.0f;

        private float randomNumber = 0.0f;
        private float fadeDuration = 2.0f;

        private Tree tree = null;
        private Animator animator = null;
        private BoxCollider attackRangeCollider = null;
        private Transform playerTransform = null;

        private Vector3 meleeAttackRange = new Vector3(0.4f, 0.8f, 0.4f);

        private void Awake()
        {
            tree = new Tree(SetTree());
            animator = transform.parent.GetComponent<Animator>();
            attackRangeCollider = transform.parent.GetChild(0).GetComponent<BoxCollider>();
            attackRangeCollider.gameObject.AddComponent<TriggerEvents>();

            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Player");
            if (taggedObjects.Length > 0)
            {
                playerTransform = taggedObjects[0].transform;
            }
        }

        private void Update()
        {
            tree.Operate();
        }

        bool IsAnimationRunning(string stateName)
        {
            if (animator != null)
            {
                if (animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
                {
                    var normalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

                    return normalizedTime != 0 && normalizedTime < 1f;
                }
            }
            return false;
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

        // 근접 기본 공격
        Node.NodeState CheckMeleeAttack()
        {
            if (IsAnimationRunning("Attack")) return Node.NodeState.RUNNING;
            return Node.NodeState.SUCCESS;
        }

        Node.NodeState CheckPlayerWithinMeleeAttackRange()
        {
            if (playerTransform != null)
            {
                if (Vector3.SqrMagnitude(playerTransform.position - transform.parent.position) < meleeAttackRange.sqrMagnitude)
                {
                    attackRangeCollider.enabled = true;
                    return Node.NodeState.SUCCESS;
                }
            }
            attackRangeCollider.enabled = false;
            return Node.NodeState.FAILURE;
        }

        Node.NodeState DoMeleeAttack()
        {
            if (playerTransform != null)
            {
                animator.SetTrigger("attack");

                randomNumber = Random.Range(0f, 1.0f);
                if (randomNumber < 0.3f) // 암전
                {
                    StartCoroutine(FadeOut());
                }
                canvasImage.enabled = true;
                StartCoroutine(FadeOut());

                return Node.NodeState.SUCCESS;
            }
            return Node.NodeState.FAILURE;
        }

        IEnumerator FadeOut()
        {
            /*float timer = 0f;
            Color imageColor = canvasImage.color;
            Color darkColor = new Color(0, 0, 0, 240);
            
            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                float progress = timer / fadeDuration;
                
                imageColor.a = Mathf.Lerp(0, 0.8f, progress);

                canvasImage.color = imageColor;
                yield return null;
            }*/



            float timer = 0f;
            Color imageColor = canvasImage.color; // 이미지의 초기 색을 가져옴
            Color transparentColor = new Color(imageColor.r, imageColor.g, imageColor.b, 0f); // 완전 투명한 색

            while (timer < fadeDuration)
            {
                timer += Time.deltaTime;
                float progress = timer / fadeDuration;

                imageColor = Color.Lerp(imageColor, transparentColor, progress); // 이미지를 서서히 투명하게 변경

                canvasImage.color = imageColor; // 이미지에 변경된 색을 적용
                yield return null;
            }

            canvasImage.enabled = false;
        }

        // RUN
        Node.NodeState MoveToPlayer()
        {
            if (playerTransform != null)
            {
                // 몬스터가 플레이어를 향해 회전
                Vector3 playerDirection = playerTransform.position - transform.parent.position;
                Quaternion targetRotation = Quaternion.LookRotation(playerDirection);
                transform.parent.rotation = targetRotation;

                // 몬스터가 플레이어를 향해 이동
                transform.parent.position = Vector3.MoveTowards(transform.parent.position, playerTransform.position, Time.deltaTime * movementSpeed);
                animator.SetTrigger("run");

                return Node.NodeState.RUNNING;
            }

            return Node.NodeState.FAILURE;
        }

        // IDLE
        Node.NodeState MoveToOriginPosition()
        {
            if (playerTransform != null)
            {
                if (Vector3.SqrMagnitude(playerTransform.position - transform.parent.position) < meleeAttackRange.sqrMagnitude)
                {
                    animator.SetTrigger("idle");
                    return Node.NodeState.SUCCESS;
                }

            }
            return Node.NodeState.FAILURE;
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