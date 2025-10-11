using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
namespace Dialogue
{
    public class DialogueStory : MonoBehaviour
    {
        [SerializeField] private Story[] _stories;
        private Dictionary<string, Story> _storiesDictionary;
        public event Action<Story> ChangedStory;
        public static Func<string> Armen;
        [Serializable]
        public struct Story
        {
            [field: SerializeField] public string Tag { get; private set; }
            [field: SerializeField] public string Text { get; private set; }
            [field: SerializeField] public Answer[] Answers { get; private set; }
        }
        [Serializable]
        public struct Answer
        {
            [field: SerializeField] public string Text { get; private set; }
            [field: SerializeField] public string ReposeText { get; private set; }


        }
        private void Start()
        {
            _storiesDictionary = _stories.ToDictionary(key => key.Tag, element => element);
        }
        public void ChangeStory(string tag)
        {
            ChangedStory?.Invoke(_storiesDictionary[tag]);

        }
    }
}

