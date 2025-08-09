using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

namespace ani
{
    public class fullmob : MonoBehaviour
    {
        private GameObject mob;
        public float timeRNDtr = 0;

        [Header("Кадры анимации")]
        public List<Sprite> frames = new List<Sprite>();

        [Header("Настройки")]
        public float framesPerSecond = 10f;
        public bool isPlaying = false;

        [Header("Связь с движением")]
        public float speedMultiplier = 1f;
        private SpriteRenderer spriteRenderer;
        private float timer = 0f;
        private int currentFrame = 0;
        private Rigidbody2D rb;

        private bool animationCompleted = false;

        [Header("Параметры фиксации игрока")]
        public GameObject player;
        private bool isPlayerFrozen = false;
        private Vector3 playerFixedPos;
        private Vector3 mobCenter;
        public int requiredSpacePresses = 10;
        private int currentSpacePressCount = 0;
        private bool activated = false;

        void Awake()
        {
            mob = this.gameObject;
            spriteRenderer = GetComponent<SpriteRenderer>();
            rb = GetComponent<Rigidbody2D>();
            isPlaying = false;
        }

        void FixedUpdate()
        {
            if (!activated) return;
            if (frames == null || frames.Count == 0) return;

            if (!isPlaying)
            {
                spriteRenderer.sprite = frames[0];
                return;
            }

            if (animationCompleted) return;

            float speedFactor = rb ? rb.velocity.magnitude * speedMultiplier : 1f;
            float interval = 1f / Mathf.Max(framesPerSecond * speedFactor, 0.01f);
            timer += Time.fixedDeltaTime;

            if (timer >= interval)
            {
                timer -= interval;
                if (currentFrame < frames.Count - 1)
                {
                    currentFrame++;
                    spriteRenderer.sprite = frames[currentFrame];
                }
                else
                {
                    currentFrame = frames.Count - 1;
                    spriteRenderer.sprite = frames[currentFrame];
                    animationCompleted = true;
                    FreezePlayer();
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!activated && collision.gameObject.CompareTag("Player"))
            {
                activated = true;
                isPlaying = true;
                animationCompleted = false;
                currentFrame = 0;
                timer = 0;
                mobCenter = transform.position; // Запоминаем центр можжевельника
                Debug.Log("Триггер сработал. Запуск анимации.");
            }
        }

        private void FreezePlayer()
        {
            if (player != null)
            {
                playerFixedPos = player.transform.position;
                playerFixedPos.x = mobCenter.x; // Притягиваем игрока к центру по горизонтали
                isPlayerFrozen = true;
            }
        }

        private void ReleasePlayer()
        {
            isPlayerFrozen = false;
            currentSpacePressCount = 0;
            Debug.Log("Игрок освобожден!");
        }

        void Update()
        {
            if (isPlayerFrozen)
            {
                if (player != null)
                {
                    player.transform.position = playerFixedPos;

                    ////PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                    //if (playerHealth != null && playerHealth.GetHealth < 1)
                    //{
                    //    Debug.Log("Здоровье игрока ниже 1. Освобождаем игрока.");
                    //    ReleasePlayer();
                    //    return;
                    //}
                }
                if (Input.GetKeyDown(KeyCode.W))
                {
                    currentSpacePressCount++;
                    if (currentSpacePressCount >= requiredSpacePresses)
                    {
                        ReleasePlayer();
                    }
                }
            }
        }

    }
}
