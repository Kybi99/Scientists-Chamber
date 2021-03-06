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
        private bool isDialgoueDone;
        private float timeToDisplay;
        public static float time;
        private string sentence;
        private Animator imageAnimator;
        private Animator teslaAnimator;
        private GameObject tesla;
        private SpriteRenderer teslaRenderer;
        private TimerManager timerManager;
        public static bool isContinueButtonEnabled;
        public static bool isCorrectObjectIn;
        public static DialogueTrigger dialogueTrigger;
        public static GameObject[] objects;
        public static GameObject[] otherObjects;
        public TMP_Text continueClick;
        public TMP_Text dialogueText;
        public Animator endScreenAnimator;
        public Material electricityMaterial;
        public ParticleSystem conffetis;

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
            continueClick.enabled = false;
            isContinueButtonEnabled = true;
            isDialgoueDone = false;
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
                otherObjects = GameObject.FindGameObjectsWithTag("otherObjects");

                for (int i = 0; i < objects.Length; i++)
                {
                    objects[i].GetComponent<SpriteRenderer>().material = electricityMaterial;
                }
                sentence = story.sentences[index] + "\n?????????? ???? ???????????? ???? ?????? ?????????? ?????? ?????? ????????.";
            }

            StopAllCoroutines();

            StartCoroutine(TypeSentence(sentence));
            portalCanvasGroup.interactable = false;

        }
        IEnumerator TypeSentence(string sentence)
        {
            continueClick.enabled = false;
            dialogueText.text = "";
            yield return new WaitForSeconds(0.5f);
            yield return new WaitForSeconds(0.2f);
            continueClick.enabled = true;
            foreach (char letter in sentence.ToCharArray())
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(0.02f);
            }
            if (DragAnDrop.numberOfPartsIn <= numberOfCorectParts - 1)
                portalCanvasGroup.interactable = true;

            isContinueButtonEnabled = true;
            isDialgoueDone = true;
        }


        private void ContinueButtonClick()
        {
            //SkipDialogue
            if (Input.GetKeyDown(KeyCode.Mouse0) && !isContinueButtonEnabled && continueClick.enabled)
            {
                StopAllCoroutines();
                dialogueText.text = sentence;
                isContinueButtonEnabled = true;
                isDialgoueDone = true;
                if (DragAnDrop.numberOfPartsIn <= numberOfCorectParts - 1)
                    portalCanvasGroup.interactable = true;
            }
            //ShowEndScreen
            else if (Input.GetKeyDown(KeyCode.Mouse0) && isContinueButtonEnabled && isDialgoueDone)
            {
                if (DragAnDrop.numberOfPartsIn == numberOfCorectParts)
                {
                    ShowHint.canClick = false;
                    endScreenAnimator.Play("EndScreenFadeIn");
                    conffetis.Emit(100);


                    timeToDisplay = TimerManager.timeOnStart - TimerManager.timeValue;

                    timerManager = FindObjectOfType<TimerManager>();
                    TimerManager.gameHasEnded = true;
                    timerManager.DisplayEndTime(timeToDisplay);
                }

                else
                    TimerManager.timeIsRunning = true;

                continueClick.enabled = false;
                isCorrectObjectIn = false;
                imageAnimator.SetBool("isCorrectObjectIn", false);
                teslaAnimator.SetBool("isCorrectObjectIn", false);
            }
        }
    }
}