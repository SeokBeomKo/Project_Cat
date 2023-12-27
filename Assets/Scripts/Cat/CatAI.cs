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

        [Header("근접공격이펙트")]
        public ParticleSystem meleeParticle;

        [Header("데이터")]
        public BattleCatData data;

        private bool playerInMeleeRange = false;
        private bool chargeAttackTime = false;
        private bool isWaveAttacking = false;
        private bool isAttackReady = true;

        private float randomNumber = 0f;
        private float attackEndTimer = 0f;
        private float attackResumptionTime = 2f;
        private float movementSpeed = 10.0f;
        private float waveAttackTime = 15f;
        private float waveTimer = 0f;

        private Tree tree = null;
        private Animator animator = null;
        private Transform playerTransform = null;

        private Vector3 chargeAttackPosition;

        private void Awake()
        {
            data.LoadDataFromPrefs();
            
            attackResumptionTime = data.attackResumptionTime;
            waveAttackTime = data.waveAttackTime;
            movementSpeed = data.movementSpeed;

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
            ReAttack();

            tree.Operate();
        }

        private void ReAttack()
        {
            if(!isAttackReady)
            {
                attackEndTimer += Time.deltaTime;
                if(attackEndTimer >= attackResumptionTime)
                {
                    isAttackReady = true;
                    attackEndTimer = 0f;
                }
            }

            if(!isWaveAttacking)
            {
                waveTimer += Time.deltaTime;
            }
            else if(isWaveAttacking && !waveCollider.activeSelf)
            {
                isWaveAttacking = false;
                waveTimer = 0f;
            }
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
            if ((IsAnimationRunning("Attack") || IsAnimationRunning("ChargeAttack") || isWaveAttacking) && isAttackReady)
            {
                return Node.NodeState.RUNNING;
            }

            if (IsAnimationRunning("Run") && isAttackReady)
            {
                return Node.NodeState.SUCCESS;
            }

            if (IsAnimationRunning("Idle"))
            {
                meleeDamageBox.SetActive(false);
                chargeDamageBox.SetActive(false);
                return Node.NodeState.FAILURE;
            }

            return Node.NodeState.FAILURE;
            
        }

        Node.NodeState CheckPlayerWithinMeleeAttackRange()
        {
            if (playerInMeleeRange)
            {
                return Node.NodeState.SUCCESS;
            }

            return Node.NodeState.FAILURE;
        }

        Node.NodeState CheckChargeTime()
        {
            if (playerTransform != null && chargeAttackTime)
            {
                chargeAttackPosition = playerTransform.position;
                
                return Node.NodeState.SUCCESS;
            }
            return Node.NodeState.FAILURE;
        }

        Node.NodeState DoChargeAttack()
        {
            if (playerTransform != null)
            {
                chargeDamageBox.SetActive(true);
                StartCoroutine(PerformChargeAttack());
                chargeAttackTime = false;
                isAttackReady = false;
                
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
            SoundManager.Instance.PlaySFX("CatCollision");
            transform.parent.position = chargeAttackPosition;
            SoundManager.Instance.PlaySFX("CatMeow");
        }

        Node.NodeState DoMeleeAttack()
        {
            randomNumber = Random.Range(0f, 1.0f);
            if (randomNumber > 0.3f)
            {
                meleeDamageBox.SetActive(true);
                meleeParticle.Play();
                animator.SetTrigger("attack");
                isAttackReady = false;
                chargeAttackTime = true;

                SoundManager.Instance.PlaySFX("CatBasicAttack");
                return Node.NodeState.SUCCESS;
            }
            return Node.NodeState.FAILURE;
        }

        Node.NodeState DoDarkAttack()
        {
            if (playerTransform != null)
            {
                meleeDamageBox.SetActive(true);
                meleeParticle.Play();
                animator.SetTrigger("attack");
                isAttackReady = false;
                chargeAttackTime = true;

                canvasImage.enabled = true;
                SoundManager.Instance.PlaySFX("CatBasicAttack");
                StartCoroutine(FadeOutOverTime(5.0f));

                return Node.NodeState.SUCCESS;
            }
            return Node.NodeState.FAILURE;
        }

        IEnumerator FadeOutOverTime(float duration)
        {
            float timer = 0f;
            Color startColor = canvasImage.color;
            Color transparentColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

            while (timer < duration)
            {
                timer += Time.deltaTime;
                float progress = timer / duration;

                canvasImage.color = Color.Lerp(startColor, transparentColor, Mathf.SmoothStep(0f, 1f, progress));

                yield return null;
            }

            canvasImage.color = transparentColor;
            canvasImage.enabled = false;
        }

        Node.NodeState CheckWaveAttackTime()
        {
            if (waveTimer >= waveAttackTime)
            {
                chargeAttackTime = false;
                waveTimer = 0f;

                return Node.NodeState.SUCCESS;
            }

            return Node.NodeState.FAILURE;
        }

        Node.NodeState DoWaveAttack()
        {
            if (playerTransform != null)
            {
                StartCoroutine(PerformWaveAttack());
                return Node.NodeState.SUCCESS;
            }

            return Node.NodeState.FAILURE;
        }

        IEnumerator PerformWaveAttack()
        {
            animator.SetTrigger("waveAttack");
            SoundManager.Instance.PlaySFX("CatWaveDrop");

            yield return null;
            float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;

            float time = 0f;

            while (time < animationLength)
            {
                yield return null;
                time += Time.deltaTime;

            }
            waveCollider.SetActive(true);
            isWaveAttacking = true;
            isAttackReady = false;
        }

        // RUN
        Node.NodeState CheckAttackEndTime()
        {
            if (isAttackReady)
            {
                return Node.NodeState.SUCCESS;
            }
            return Node.NodeState.FAILURE;
        }

        Node.NodeState MoveToPlayer()
        {
            if (playerTransform != null)
            {
                animator.SetBool("run", true);

                Vector3 playerDirection = playerTransform.position - transform.parent.position;
                playerDirection.y = 0f;
                playerDirection = Vector3.Normalize(playerDirection);

                if (playerDirection != Vector3.zero)
                {
                    Vector3 playerDirectionNoY = new Vector3(playerDirection.x, 0f, playerDirection.z);
                    Quaternion targetRotation = Quaternion.LookRotation(playerDirectionNoY);
                    transform.parent.rotation = targetRotation;

                    Vector3 targetPosition = new Vector3(playerTransform.position.x, transform.parent.position.y, playerTransform.position.z);
                    transform.parent.position = Vector3.MoveTowards(transform.parent.position, targetPosition, Time.deltaTime * movementSpeed);
                    
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