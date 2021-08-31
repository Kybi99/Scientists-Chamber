using System.Collections;
using UnityEngine;
using FourGear.Mechanics;
using TMPro;
using FourGear.UI;
namespace FourGear.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] CanvasGroup portalCanvasGroup;
        private int numberOfCorectParts;
        public static float time;
        private string sentence;
        private Animator imageAnimator;
        private Animator teslaAnimator;
        private GameObject tesla;
        private SpriteRenderer teslaRenderer;
        public static bool isContinueButtonEnabled;
        public static bool isCorrectObjectIn;
        public static DialogueTrigger dialogueTrigger;
        public TMP_Text continueClick;
        public TMP_Text dialogueText;
        public Animator endScreenAnimator;
        public Material electricityMaterial;
        public static GameObject[] objects;



        void Start()
        {
            numberOfCorectParts = 4;
            time = 0.7f;
            imageAnimator = GameObject.FindGameObjectWithTag("image").GetComponent<Animator>();
            tesla = GameObject.FindGameObjectWithTag("tesla");
            teslaRenderer = tesla.GetComponent<SpriteRenderer>();
            teslaAnimator = tesla.GetComponent<Animator>();
            dialogueTrigger = GetComponent<DialogueTrigger>();
            teslaRenderer.enabled = false;
            isContinueButtonEnabled = true;
        }
        private void Update()
        {
            ContinueButtonClick();
        }

        public void StartDialogue(Story story, int index)
        {
            //start a dialogue when right object is on place with animations

            isCorrectObjectIn = true;
            teslaRenderer.enabled = true;
            isContinueButtonEnabled = false;
            TimerManager.timeIsRunning = false;
            teslaAnimator.SetBool("isCorrectObjectIn", true);
            imageAnimator.SetBool("isCorrectObjectIn", true);

            if (DragAnDrop.numberOfPartsIn < numberOfCorectParts - 1)
                sentence = story.sentences[index];
            else
            {
                objects = GameObject.FindGameObjectsWithTag("objects");

                for (int i = 0; i < objects.Length; i++)
                {
                    objects[i].GetComponent<SpriteRenderer>().material = electricityMaterial;
                }
                sentence = story.sentences[index] + "\nХвала на помоћи не бих успео ово без тебе.";
            }

            StopAllCoroutines();

            StartCoroutine(TypeSentence(sentence));
            portalCanvasGroup.interactable = false;

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
            if (DragAnDrop.numberOfPartsIn <= numberOfCorectParts - 1)
                portalCanvasGroup.interactable = true;

            continueClick.enabled = true;
            isContinueButtonEnabled = true;

        }


        private void ContinueButtonClick()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && isContinueButtonEnabled && continueClick.enabled)
            {
                if (DragAnDrop.numberOfPartsIn == numberOfCorectParts)
                {
                    ShowHint.canClick = false;
                    endScreenAnimator.Play("EndScreenFadeIn");
                }

                else
                    TimerManager.timeIsRunning = true;

                //dont let player cancel the text animation and leave room while tesla is done speaking
                continueClick.enabled = false;
                isCorrectObjectIn = false;
                imageAnimator.SetBool("isCorrectObjectIn", false);
                teslaAnimator.SetBool("isCorrectObjectIn", false);
            }
        }
    }
}