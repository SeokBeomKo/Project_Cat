using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpecialAttack
{
    public class WaveAttack : MonoBehaviour
    {
        [Header("데이터")]
        public BattleCatWaveData data;

        [Header("히트 박스")]
        public GameObject hitBox;

        private float timer = 0f;
        private float maxTimer = 3.5f;
        private float growthSpeed = 1f;

        private float minSize = 1f;
        private float maxSize = 3f;
        private float size = 0f;

        private Animator animator = null;
        private Renderer waveRenderer = null;
        private Color initialColor;

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
            waveRenderer = hitBox.GetComponent<Renderer>();
            initialColor = waveRenderer.material.color;

            maxTimer = (maxSize - minSize) / growthSpeed;
        }

        private void OnEnable()
        {
            SetInitialSize();

            if(waveRenderer != null)
            {
                waveRenderer.material.color = initialColor;
            }
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
                Color newColor = new Color(initialColor.r, Mathf.Lerp(initialColor.g, initialColor.g - growthSpeed * 5, timer / maxTimer), initialColor.b, initialColor.a);
                waveRenderer.material.color = newColor;
            }
            else
            {
                animator.SetBool("idle", true);
                gameObject.SetActive(false);
            }
        }
    }
}