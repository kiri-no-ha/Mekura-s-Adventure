using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace animator
{
    public class anim : MonoBehaviour
    {
        [System.Serializable]
        public class AnimationSequence
        {
            public string name; // Уникальное имя анимации
            public List<Sprite> frames; // Кадры анимации
            public float framesPerSecond = 10f; // Скорость для конкретной анимации
            public bool loop = false; // Зацикливать ли анимацию
        }

        [Header("Список анимаций")]
        public List<AnimationSequence> animations = new List<AnimationSequence>();

        [Header("Текущие настройки")]
        public string defaultAnimation = "Idle";
        public bool isPlaying = false;

        private SpriteRenderer spriteRenderer;
        private float timer = 0f;
        private int currentFrame = 0;
        private bool animationCompleted = false;
        private string currentAnimation;
        private AnimationSequence currentSequence;

        void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            SwitchAnimation(defaultAnimation);
        }

        void FixedUpdate()
        {
            if (!isPlaying || currentSequence == null)
                return;

            // Обновление таймера
            timer += Time.fixedDeltaTime;
            float interval = 1f / currentSequence.framesPerSecond;

            // Проверка необходимости смены кадра
            if (timer >= interval)
            {
                timer -= interval;
                NextFrame();
            }
        }

        private void NextFrame()
        {
            // Переход к следующему кадру
            currentFrame++;

            // Проверка завершения анимации
            if (currentFrame >= currentSequence.frames.Count)
            {
                if (currentSequence.loop)
                {
                    currentFrame = 0; // Циклический переход
                }
                else
                {
                    currentFrame = currentSequence.frames.Count - 1; // Фиксация на последнем кадре
                    animationCompleted = true;
                    isPlaying = false;
                }
            }

            // Обновление отображаемого спрайта
            if (currentFrame < currentSequence.frames.Count)
            {
                spriteRenderer.sprite = currentSequence.frames[currentFrame];
            }
        }

        // === ПУБЛИЧНЫЕ МЕТОДЫ ДЛЯ УПРАВЛЕНИЯ АНИМАЦИЯМИ ===

        /// <summary>
        /// Переключение на указанную анимацию
        /// </summary>
        public void SwitchAnimation(string animationName)
        {
            // Поиск анимации в списке
            AnimationSequence newSequence = animations.Find(a => a.name == animationName);
            

            if (newSequence == null)
            {
                Debug.LogWarning($"Анимация {animationName} не найдена!");
                Debug.Log($"Анимация {animationName} не найдена!");
                return;
            }

            // Инициализация новой анимации
            currentAnimation = animationName;
            currentSequence = newSequence;
            currentFrame = 0;
            timer = 0;
            animationCompleted = false;
            isPlaying = true;

            // Немедленное отображение первого кадра
            spriteRenderer.sprite = currentSequence.frames[0];
        }

        /// <summary>
        /// Принудительный запуск текущей анимации с начала
        /// </summary>
        public void Play()
        {
            if (currentSequence == null) return;
            currentFrame = 0;
            timer = 0;
            animationCompleted = false;
            isPlaying = true;
            spriteRenderer.sprite = currentSequence.frames[0];
        }

        /// <summary>
        /// Остановка анимации (остается на текущем кадре)
        /// </summary>
        public void Stop()
        {
            isPlaying = false;
        }

        /// <summary>
        /// Продолжение анимации с текущего кадра
        /// </summary>
        public void Resume()
        {
            if (currentSequence != null)
                isPlaying = true;
        }

        /// <summary>
        /// Проверка завершения анимации (для нецикличных)
        /// </summary>
        public bool IsAnimationComplete()
        {
            return animationCompleted;
        }
    }
}
