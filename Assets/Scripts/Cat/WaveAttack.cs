using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpecialAttack
{
    public class WaveAttack : MonoBehaviour
    {
        [Header("데이터")]
        public BattleCatWaveData data;

        private float timer = 0f;
        private float maxTimer = 3.5f;
        private float growthSpeed = 1f;

        private float minSize = 1f;
        private float maxSize = 3f;
        private float size = 0f;

        private Animator animator = null;

        void Awake()
        {
            data.LoadDataFromPrefs();
        }

        private void Start()
        {
            minSize = data.minAttackSize;
            maxSize = data.maxAttackSize;
            growthSpeed = data.growthSpeed;

            animator = transform.parent.GetComponent<Animator>();

            maxTimer = (maxSize - minSize) / growthSpeed;
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
            transform.localScale = new Vector3(1, transform.localScale.y, 1);

            size = minSize;
            timer = 0f;
        }

        private void IncreaseSize()
        {
            if (timer <= maxTimer)
            {
                size = Mathf.Lerp(size, maxSize, Time.deltaTime * growthSpeed);
                transform.localScale = new Vector3(size, transform.localScale.y, size);
            }
            else
            {
                animator.SetBool("idle", true);
                gameObject.SetActive(false);
            }
        }
    }
}