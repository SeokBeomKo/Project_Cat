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

        [Header("파동콜라이더")]
        public GameObject waveCollider;

        [Header("공격 재개 시간")]
        public float attackResumptionTime = 2f;

        [Header("파동 공격 출력 시간")]
        public float waveAttackTime = 15f;

        [Header("근접공격이펙트")]
        public ParticleSystem meleeParticle;

        [Header("속도")]
        [SerializeField]
        private float movementSpeed = 10.0f;

        private bool playerInMeleeRange = false;
        private bool chargeAttackTime = false;
        private bool isAttackComplete = true;
        private bool isAttacking = false;

        private float randomNumber = 0f;
        private float attackEndTimer = 0f;
        private float timer = 0f;

        private Tree tree = null;
        private Animator animator = null;
        private Transform playerTransform = null;

        private Vector3 chargeAttackPosition;

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
                if (attackEndTimer >= attackResumptionTime)
                {
                    isAttackComplete = true;
                    isAttacking = false;
                    attackEndTimer = 0f;
                }
            }

            timer += Time.deltaTime;
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

            var waveAttack = new Sequence(
                new List<Node>()
                {
                    new ActionNode(CheckWaveAttackTime),
                    new ActionNode(DoWaveAttack)
                });

            var triggerAttack = new Sequence(
                new List<Node>()
                {
                    new ActionNode(CheckPlayerWithinMeleeAttackRange),

                    new Selector(new List<Node>()
                    {
                        chargeAttack,
                        meleeAttack
                    })
                });

            var attack = new Sequence(
                new List<Node>()
                {
                    new ActionNode(CheckAnimation),

                    new Selector(new List<Node>()
                    {
                        triggerAttack,
                        waveAttack
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
            else if (IsAnimationRunning("Attack") || IsAnimationRunning("ChargeAttack") || waveCollider.activeSelf)
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

        /*Node.NodeState CheckChargeTime()
        {
            if (playerTransform != null)
            {
                if (chargeAttackTime)
                {
                    animator.SetTrigger("chargeAttack");
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
                isAttacking = true;
                isAttackComplete = false;

                Vector3 chargeAttackPosition = new Vector3(chargeAttackrPosition.x, transform.parent.position.y, chargeAttackrPosition.z);

                Debug.Log("돌진 공격");
                transform.parent.position = chargeAttackPosition;
                return Node.NodeState.SUCCESS;
            }

            return Node.NodeState.FAILURE;
        }*/

        Node.NodeState CheckChargeTime()
        {
            if (playerTransform != null)
            {
                if (chargeAttackTime)
                {
                    chargeAttackPosition = playerTransform.position;
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
                chargeDamageBox.SetActive(true);
                StartCoroutine(PerformChargeAttack());

                isAttacking = true;
                isAttackComplete = false;

                Debug.Log("돌진 공격");
                
                return Node.NodeState.SUCCESS;
            }

            return Node.NodeState.FAILURE;
        }

        IEnumerator PerformChargeAttack()
        {
            animator.SetTrigger("chargeAttack");

            yield return null;
            float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;

            float time = 0f;
            Vector3 startPosition = transform.parent.position;

            while (time < animationLength)
            {
                float height = Mathf.Sin(Mathf.PI * time / animationLength) * 0.5f;

                Vector3 currentPosition = Vector3.Lerp(startPosition, chargeAttackPosition, time / animationLength);
                currentPosition.y += height;

                transform.parent.position = currentPosition;

                yield return null;

                time += Time.deltaTime;
            }
            transform.parent.position = chargeAttackPosition;
        }

            Node.NodeState DoMeleeAttack()
        {
            randomNumber = Random.Range(0f, 1.0f);
            if (playerTransform != null && randomNumber > 0.3f)
            {
                Debug.Log("근접 기본 공격");
                meleeParticle.Play();
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
                Debug.Log("근접 암전 공격");
                meleeParticle.Play();
                animator.SetTrigger("attack");
                isAttacking = true;
                isAttackComplete = false;
                chargeAttackTime = true;
                canvasImage.enabled = true;
                StartCoroutine(FadeOutOverTime(5.0f));
                
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

                //imageColor = Color.Lerp(imageColor, transparentColor, progress);
                imageColor = Color.Lerp(imageColor, transparentColor, Mathf.SmoothStep(0f, 1f, progress));

                canvasImage.color = imageColor;
                yield return null;
            }

            canvasImage.enabled = false;
        }

        Node.NodeState CheckWaveAttackTime()
        {
            if (playerTransform != null)
            {
                if (timer >= waveAttackTime)
                {
                    chargeAttackTime = false;

                    return Node.NodeState.SUCCESS;
                }
            }

            return Node.NodeState.FAILURE;
        }

        Node.NodeState DoWaveAttack()
        {
            if (playerTransform != null)
            {
                Debug.Log("특수 파동 공격");
                waveCollider.SetActive(true);
                timer = 0f;

                return Node.NodeState.SUCCESS;
            }

            return Node.NodeState.FAILURE;
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
                playerDirection = Vector3.Normalize(playerDirection);

                if (playerDirection != Vector3.zero)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(playerDirection);
                    transform.parent.rotation = targetRotation;

                    Vector3 targetPosition = new Vector3(playerTransform.position.x, transform.parent.position.y, playerTransform.position.z);
                    transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPosition, Time.deltaTime * movementSpeed);
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