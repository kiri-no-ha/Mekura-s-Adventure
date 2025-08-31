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
            public string name; // ���������� ��� ��������
            public List<Sprite> frames; // ����� ��������
            public float framesPerSecond = 10f; // �������� ��� ���������� ��������
            public bool loop = false; // ����������� �� ��������
        }

        [Header("������ ��������")]
        public List<AnimationSequence> animations = new List<AnimationSequence>();

        [Header("������� ���������")]
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

            // ���������� �������
            timer += Time.fixedDeltaTime;
            float interval = 1f / currentSequence.framesPerSecond;

            // �������� ������������� ����� �����
            if (timer >= interval)
            {
                timer -= interval;
                NextFrame();
            }
        }

        private void NextFrame()
        {
            // ������� � ���������� �����
            currentFrame++;

            // �������� ���������� ��������
            if (currentFrame >= currentSequence.frames.Count)
            {
                if (currentSequence.loop)
                {
                    currentFrame = 0; // ����������� �������
                }
                else
                {
                    currentFrame = currentSequence.frames.Count - 1; // �������� �� ��������� �����
                    animationCompleted = true;
                    isPlaying = false;
                }
            }

            // ���������� ������������� �������
            if (currentFrame < currentSequence.frames.Count)
            {
                spriteRenderer.sprite = currentSequence.frames[currentFrame];
            }
        }

        // === ��������� ������ ��� ���������� ���������� ===

        /// <summary>
        /// ������������ �� ��������� ��������
        /// </summary>
        public void SwitchAnimation(string animationName)
        {
            // ����� �������� � ������
            AnimationSequence newSequence = animations.Find(a => a.name == animationName);
            

            if (newSequence == null)
            {
                Debug.LogWarning($"�������� {animationName} �� �������!");
                Debug.Log($"�������� {animationName} �� �������!");
                return;
            }

            // ������������� ����� ��������
            currentAnimation = animationName;
            currentSequence = newSequence;
            currentFrame = 0;
            timer = 0;
            animationCompleted = false;
            isPlaying = true;

            // ����������� ����������� ������� �����
            spriteRenderer.sprite = currentSequence.frames[0];
        }

        /// <summary>
        /// �������������� ������ ������� �������� � ������
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
        /// ��������� �������� (�������� �� ������� �����)
        /// </summary>
        public void Stop()
        {
            isPlaying = false;
        }

        /// <summary>
        /// ����������� �������� � �������� �����
        /// </summary>
        public void Resume()
        {
            if (currentSequence != null)
                isPlaying = true;
        }

        /// <summary>
        /// �������� ���������� �������� (��� �����������)
        /// </summary>
        public bool IsAnimationComplete()
        {
            return animationCompleted;
        }
    }
}
