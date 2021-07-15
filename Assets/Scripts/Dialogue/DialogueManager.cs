using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FourGear.Mechanics;
using TMPro;

namespace FourGear.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        public TMP_Text nameText;
        public TMP_Text dialogueText;
        private string[] sentences;
        private  string sentence;
        private Animator animator;    
        public static bool isCorrectObjectIn;
        public static DialogueTrigger dialogueTrigger;
        
        void Start()
        {
            animator = GameObject.FindGameObjectWithTag("image").GetComponent<Animator>();
           
            dialogueTrigger = GetComponent<DialogueTrigger>();
            //sentences = new Queue<string>();
        }

        public void StartDialogue(Story story, int index)
        {
            isCorrectObjectIn = true;
            animator.SetBool("isCorrectObjectIn", true);
            Debug.Log(animator.GetBool("isCorrectObjectIn"));
            //Debug.Log("yay");
            nameText.text = story.nameOfNpc;
            //sentences.Clear();
            
            Debug.Log(DragAnDrop.numberOfPartsIn);
            if(DragAnDrop.numberOfPartsIn < 2)
                sentence = story.sentences[index];
            else 
                sentence = story.sentences[index]+ " Uspesno si zavrsio nivo.";

            dialogueText.text = sentence;            
        }
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                isCorrectObjectIn = false;
                animator.SetBool("isCorrectObjectIn", false);
                Debug.Log(animator.GetBool("isCorrectObjectIn"));

            }
                

        }

        /*public void DisplayNextSentence()
        {
            if(sentences[i] != null)
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
        }*/
     
    }
}
