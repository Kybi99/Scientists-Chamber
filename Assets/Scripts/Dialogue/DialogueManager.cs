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
        private string sentence;
        private Animator imageAnimator;
        private Animator teslaAnimator;
        private GameObject tesla;
        private SpriteRenderer teslaRenderer;
        public static bool isContinueButtonEnabled;
        public static bool isCorrectObjectIn;
        public static DialogueTrigger dialogueTrigger;
        public TMP_Text continueClick;
        //private Button myButton;

        void Start()
        {
            //continueClick.enabled = false;
            imageAnimator = GameObject.FindGameObjectWithTag("image").GetComponent<Animator>();
            tesla = GameObject.FindGameObjectWithTag("tesla");
            //tesla.transform.position =  new Vector2(10, 0.46f);
            //tesla.GetComponent<SpriteRenderer>().enabled = false;
            teslaRenderer = tesla.GetComponent<SpriteRenderer>();
            teslaRenderer.enabled = false;
            teslaAnimator = tesla.GetComponent<Animator>();
            dialogueTrigger = GetComponent<DialogueTrigger>();
            isContinueButtonEnabled = true;
        }

        public void StartDialogue(Story story, int index)                                                                       //start a dialogue when right object is on place with animations
        {
            isCorrectObjectIn = true;
            teslaRenderer.enabled = true;
            isContinueButtonEnabled = false;
            teslaAnimator.SetBool("isCorrectObjectIn", true);
            imageAnimator.SetBool("isCorrectObjectIn", true);

            //tesla.SetActive(true);
            nameText.text = story.nameOfNpc;


            //Debug.Log(DragAnDrop.numberOfPartsIn);
            if (DragAnDrop.numberOfPartsIn < 2)
                sentence = story.sentences[index];
            else
                sentence = story.sentences[index] + " Uspesno si zavrsio nivo.";

            StopAllCoroutines();
            //if(imageAnimator.GetCurrentAnimatorStateInfo(0).IsName("DialogueCominIn"))
            StartCoroutine(TypeSentence(sentence));
        }
        IEnumerator TypeSentence(string sentence)
        {
            yield return new WaitForSeconds(0.25f);

            dialogueText.text = "";
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(0.03f);
            }
            isContinueButtonEnabled = true;
            continueClick.enabled = true;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && isContinueButtonEnabled)                                        //dont let player cancel the text animation and leave room while tesla is done speaking
            {
                continueClick.enabled = false;
                isCorrectObjectIn = false;
                //Debug.Log(imageAnimator.GetBool("isCorrectObjectIn"));
                imageAnimator.SetBool("isCorrectObjectIn", false);
                teslaAnimator.SetBool("isCorrectObjectIn", false);
            }


        }
    }
}