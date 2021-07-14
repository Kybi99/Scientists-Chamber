using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FourGear.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        public TMP_Text nameText;
        public TMP_Text dialogueText;
        private Queue<string> sentences;
        
        void Start()
        {
            sentences = new Queue<string>();
        }

        public void StartDialogue(Story story)
        {
            Debug.Log("yay");
            nameText.text = story.nameOfNpc;
            sentences.Clear();
            
            foreach(string sentence in story.sentences)
            {
                sentences.Enqueue(sentence);
            }
            DisplayNextSentence();
        }

        public void DisplayNextSentence()
        {
            if(sentences.Count == 0)
            {
                EndDialogue();
                return;
            }
            string sentence = sentences.Dequeue();
            dialogueText.text = sentence;
        }
        private void EndDialogue()
        {
            Debug.Log("end of dialogue");
        }
     
    }
}
