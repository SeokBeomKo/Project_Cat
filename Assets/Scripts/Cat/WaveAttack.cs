using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpecialAttack
{
    public class WaveAttack : MonoBehaviour
    {
        [Header("µ•¿Ã≈Õ")]
        public BattleCatWaveData data;

        private float minSafeSize = 0f;
        private float maxSafeSize = 0f;
        private float safeSize = 1f;
        private float attackSize = 1f;
        private float timer = 0f;
        private float maxTimer = 3.5f;
        private float minAttackSize = 1.5f;
        private float maxAttackSize = 3.5f;
        private float growthSpeed = 1f;

        private Animator animator = null;

        private void Start()
        {
            minAttackSize = data.minAttackSize;
            maxAttackSize = data.maxAttackSize;
            growthSpeed = data.growthSpeed;

            animator = transform.parent.GetComponent<Animator>();

            minSafeSize = minAttackSize - 0.5f;
            maxSafeSize = maxAttackSize - 0.5f;
            maxTimer = (maxAttackSize - minAttackSize) / growthSpeed;
        }

        private void OnEnable()
        {
            SetInitialSize();
        }

        void Update()
        {
            timer += Time.deltaTime;

            IncreaseSize();
        }

        private void SetInitialSize()
        {
            transform.Find("SafeBox").localScale = new Vector3(minSafeSize, transform.Find("SafeBox").localScale.y, minSafeSize);
            transform.Find("HitBox").localScale = new Vector3(minAttackSize, transform.Find("HitBox").localScale.y, minAttackSize);

            safeSize = minSafeSize;
            attackSize = minAttackSize;
            timer = 0f;
        }

        private void IncreaseSize()
        {
            if (timer <= maxTimer)
            {
                safeSize = Mathf.Lerp(safeSize, maxSafeSize, Time.deltaTime * growthSpeed);
                transform.Find("SafeBox").localScale = new Vector3(safeSize, transform.Find("SafeBox").localScale.y, safeSize);

                attackSize = Mathf.Lerp(attackSize, maxAttackSize, Time.deltaTime * growthSpeed);
                transform.Find("HitBox").localScale = new Vector3(attackSize, transform.Find("HitBox").localScale.y, attackSize);
            }
            else
            {
                animator.SetBool("idle", true);
                gameObject.SetActive(false);
            }
        }
    }
}