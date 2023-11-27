using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpecialAttack
{
    public class WaveAttack : MonoBehaviour
    {
        [Header("최소 공격 범위 크기")]
        public float minAttackSize = 1.5f;

        [Header("최대 공격 범위 크기")]
        public float maxAttackSize = 3.5f;

        [Header("최소 안전 범위 크기")]
        public float minSafeSize = 1f;

        [Header("최대 안전 범위 크기")]
        public float maxSafeSize = 3f;

        [Header("크기 증가 최대 시간")]
        public float maxTimer = 3.5f;

        [Header("증가 속도")]
        public float growthSpeed = 1f;

        private bool safeCheck = false;

        private float safeSize = 1f;
        private float attackSize = 1f;
        private float timer = 0f;    
        
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
                gameObject.SetActive(false);
            }
        }
    }
}