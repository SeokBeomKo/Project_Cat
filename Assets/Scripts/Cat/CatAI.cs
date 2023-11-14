using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviorTree
{
    public class CatAI : MonoBehaviour
    {
        [Header("암전공격이미지")]
        public Image canvasImage;

        [Header("근접데미지박스")]
        public GameObject meleeDamageBox;

        [Header("돌진데미지박스")]
        public GameObject chargeDamageBox;

        [Header("속도")]
        [SerializeField]
        private float movementSpeed = 10.0f;

        private bool playerInMeleeRange = false;
        private bool chargeAttackTime = false;
        private bool isAttackComplete = true;
        private bool isAttacking = false;

        private float randomNumber = 0f;
        private float attackEndTimer = 0f;

        private Tree tree = null;
        private Animator animator = null;
        private Transform playerTransform = null;

        private Vector3 chargeAttackrPosition;

        // 돌진 중인 동안의 높이 조절을 위한 변수
        private float currentChargeHeight = 0f;

        // 돌진 중의 높이 조절 속도
        public float chargeHeightSpeed = 5f;

        private void Awake()
        {
            tree = new Tree(SetTree());
            animator = transform.parent.GetComponent<Animator>();

            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("Player");
            if (taggedObjects.Length > 0)
            {
                playerTransform = taggedObjects[0].transform;
            }
        }

        private void Update()
        {
            if (isAttacking)
            {
                attackEndTimer += Time.deltaTime;
                if (attackEndTimer >= 2f)
                {
                    isAttackComplete = true;
                    isAttacking = false;
                    attackEndTimer = 0f;
                }
            }

            tree.Operate();
        }

        bool IsAnimationRunning(string stateName)
        {
            if (animator != null)
            {
                return animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
            }
            return false;
        }

        Node SetTree()
        {
            var chargeAttack = new Sequence(
                new List<Node>()
                {
                    new ActionNode(CheckChargeTime),
                    new ActionNode(DoChargeAttack)
                });

            var meleeAttack = new Selector(
                new List<Node>()
                {
                    new ActionNode(DoMeleeAttack),
                    new ActionNode(DoDarkAttack)
                });

            var attack = new Sequence(
                new List<Node>()
                {
                    new ActionNode(CheckAnimation),
                    new ActionNode(CheckPlayerWithinMeleeAttackRange),

                    new Selector(new List<Node>()
                    {
                        chargeAttack,
                        meleeAttack
                    })
                });

            var moveToPlayer = new Sequence(
                new List<Node>()
                {
                    new ActionNode(CheckAttackEndTime),
                    new ActionNode(MoveToPlayer)
                });

            var moveToOrigin = new ActionNode(MoveToOriginPosition);

            return new Selector(new List<Node>()
            {
                attack,
                moveToPlayer,
                moveToOrigin
            });
        }

        Node.NodeState CheckAnimation()
        {
            if (IsAnimationRunning("Run"))
            {
                meleeDamageBox.SetActive(false);
                chargeDamageBox.SetActive(false);
                return Node.NodeState.SUCCESS;
            }
            else if (IsAnimationRunning("Attack") || IsAnimationRunning("ChargeAttack"))
            {
                if (IsAnimationRunning("Attack"))
                {
                    meleeDamageBox.SetActive(true);
                }
                else
                {
                    chargeDamageBox.SetActive(true);
                }
                return Node.NodeState.RUNNING;
            }

            meleeDamageBox.SetActive(false);
            chargeDamageBox.SetActive(false);
            return Node.NodeState.FAILURE;
        }

        Node.NodeState CheckPlayerWithinMeleeAttackRange()
        {
            if (playerTransform != null)
            {
                if (playerInMeleeRange && isAttackComplete)
                 {
                    return Node.NodeState.SUCCESS;
                }
            }

            return Node.NodeState.FAILURE;
        }

        Node.NodeState CheckChargeTime()
        {
            if (playerTransform != null)
            {
                if (chargeAttackTime)
                {
                    chargeAttackrPosition = playerTransform.position;
                    chargeDamageBox.SetActive(true);

                    chargeAttackTime = false;
                    return Node.NodeState.SUCCESS;
                }
            }

            return Node.NodeState.FAILURE;
        }

        Node.NodeState DoChargeAttack()
        {
            if (playerTransform != null)
            {
                animator.SetTrigger("chargeAttack");
                isAttacking = true;
                isAttackComplete = false;
                //StartCoroutine(MoveToPlayerWithHeight());

                transform.parent.position = chargeAttackrPosition;

                return Node.NodeState.SUCCESS;
            }

            return Node.NodeState.FAILURE;
        }

        /*IEnumerator MoveToPlayerWithHeight()
        {
            // 플레이어 몸을 기준으로 앞에서 얼마나 멈출지의 거리
            float stoppingDistance = 10f;

            // 플레이어 방향 벡터 계산
            Vector3 playerDirection = (playerTransform.position - transform.parent.position).normalized;

            // 플레이어 몸을 기준으로 앞에서 멈출 위치 계산
            Vector3 targetPosition = playerTransform.position - playerDirection * stoppingDistance;
            targetPosition.y = transform.parent.position.y; // 현재 높이를 유지하기 위해 y값 설정

            while (Vector3.Distance(transform.parent.position, targetPosition) > 0.1f)
            {
                // 서서히 높이 조절
                currentChargeHeight = Mathf.MoveTowards(currentChargeHeight, 5f, chargeHeightSpeed * Time.deltaTime);

                // 플레이어 방향으로 이동
                transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPosition, movementSpeed * Time.deltaTime);

                // Y축 높이 조절
                transform.parent.position += Vector3.up * currentChargeHeight;

                // 플레이어와의 충돌 감지
                RaycastHit hit;
                if (Physics.Raycast(transform.parent.position, playerDirection, out hit, stoppingDistance))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        // 플레이어가 몬스터 콜라이더에 들어오면 멈춤
                        break;
                    }
                }

                // 레이캐스트를 디버그 목적으로 그리기
                Debug.DrawRay(transform.parent.position, playerDirection * stoppingDistance, Color.red);

                yield return null;
            }

            // 코루틴이 끝나면 높이를 초기화
            currentChargeHeight = 0f;
        }*/

        Node.NodeState DoMeleeAttack()
        {
            randomNumber = Random.Range(0f, 1.0f);
            if (playerTransform != null && randomNumber > 0.3f)
            {
                animator.SetTrigger("attack");
                isAttacking = true;
                isAttackComplete = false;
                chargeAttackTime = true;
                return Node.NodeState.SUCCESS;
            }
            return Node.NodeState.FAILURE;
        }

        Node.NodeState DoDarkAttack()
        {
            if (playerTransform != null)
            {
                animator.SetTrigger("attack");
                isAttacking = true;
                isAttackComplete = false;
                chargeAttackTime = true;
                canvasImage.enabled = true;
                StartCoroutine(FadeOutOverTime(10f));

                return Node.NodeState.SUCCESS;
            }
            return Node.NodeState.FAILURE;
        }

        IEnumerator FadeOutOverTime(float duration)
        {
            float timer = 0f;
            Color imageColor = canvasImage.color;
            Color transparentColor = new Color(imageColor.r, imageColor.g, imageColor.b, 0f);

            while (timer < duration)
            {
                timer += Time.deltaTime;
                float progress = timer / duration;

                imageColor = Color.Lerp(imageColor, transparentColor, progress);

                canvasImage.color = imageColor;
                yield return null;
            }

            canvasImage.enabled = false;
        }

        // RUN
        Node.NodeState CheckAttackEndTime()
        {
            if(playerTransform != null && isAttackComplete)
            {
                return Node.NodeState.SUCCESS;
            }
            return Node.NodeState.FAILURE;
        }

        Node.NodeState MoveToPlayer()
        {
            if (playerTransform != null)
            {
                Vector3 playerDirection = playerTransform.position - transform.parent.position;
                playerDirection.y = 0f;

                if (playerDirection != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(playerDirection);
                    transform.parent.rotation = targetRotation;

                    transform.parent.position = Vector3.MoveTowards(transform.parent.position, playerTransform.position, Time.deltaTime * movementSpeed);
                    animator.SetBool("run", true);

                    return Node.NodeState.SUCCESS;
                }
            }

            return Node.NodeState.FAILURE;
        }

        // IDLE
        Node.NodeState MoveToOriginPosition()
        {
            if (playerTransform != null)
            {
                animator.SetBool("run", false);
                return Node.NodeState.SUCCESS;
            }
            return Node.NodeState.FAILURE;
        }

        private void OnTriggerEnter(Collider other)
        { 
            if (other.CompareTag("Player"))
            {
                playerInMeleeRange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                playerInMeleeRange = false;
            }
        }
    }
}