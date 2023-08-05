using System;
using System.Collections.Generic;
using System.Linq;
using player;
using TMPro;
using UnityEngine;

namespace stage
{
    public class BallReceiver : MonoBehaviour
    {
        private Dictionary<string, string> _dictionary;
        private List<string> _translatedSentence;
        public TextMeshPro text;
        public bool[] isCorrect;

        public void SetDictionary(Dictionary<string, string> dictionary)
        {
            _dictionary = dictionary;
        }

        public void SetAnswer(List<string> translatedSentence)
        {
            _translatedSentence = translatedSentence;
            isCorrect = new bool[_translatedSentence.Count];
            text.text = string.Join(" ", _translatedSentence);
        }

        public void ThisBallIsCorrect(string receivedText)
        {
            var translated = _dictionary[receivedText];
            for (int i = 0; i < _translatedSentence.Count; i++)
            {
                if (_translatedSentence[i] == translated)
                {
                    isCorrect[i] = true;
                    break;
                }
            }
        }
        private void Update()
        {
            SetText();
            if (isCorrect.All(b => b)) EventManager.Instance.Clear();
        }

        private void SetText()
        {
            string colorizedText = "";
            for (int i = 0; i < isCorrect.Length; i++)
            {
                string colorTag;
                if (isCorrect[i])
                {
                    colorTag = "<color=#000000>";
                }
                else
                {
                    colorTag = "<color=#808080>";
                }

                colorizedText += colorTag + _translatedSentence[i] + "</color> ";
            }

            text.text = colorizedText;
        }
    }
}