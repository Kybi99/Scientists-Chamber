using System.Collections;
using UnityEngine;
using FourGear.Mechanics;
using FourGear.UI;
using TMPro;

namespace FourGear.Dialogue
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] CanvasGroup canvasGroup;
        private int numberOfCorectParts;
        private float time;
        private string sentence;
        private bool startTimer;
        private Animator imageAnimator;
        private Animator teslaAnimator;
        private GameObject tesla;
        private SpriteRenderer teslaRenderer;
        public static bool isContinueButtonEnabled;
        public static bool isCorrectObjectIn;
        public static DialogueTrigger dialogueTrigger;
        public TMP_Text continueClick;
        public TMP_Text nameText;
        public TMP_Text dialogueText;
        public Animator endScreenAnimator;


        void Start()
        {
            numberOfCorectParts = 3;
            time = 1;
            imageAnimator = GameObject.FindGameObjectWithTag("image").GetComponent<Animator>();
            tesla = GameObject.FindGameObjectWithTag("tesla");
            teslaRenderer = tesla.GetComponent<SpriteRenderer>();
            teslaAnimator = tesla.GetComponent<Animator>();
            dialogueTrigger = GetComponent<DialogueTrigger>();
            teslaRenderer.enabled = false;
            isContinueButtonEnabled = true;
            startTimer = false;
        }
        private void Update()
        {
            ContinueButtonClick();
            StartCounting();
            DestoryDDOLs();
        }

        public void StartDialogue(Story story, int index)
        {
            //start a dialogue when right object is on place with animations

            isCorrectObjectIn = true;
            teslaRenderer.enabled = true;
            isContinueButtonEnabled = false;
            teslaAnimator.SetBool("isCorrectObjectIn", true);
            imageAnimator.SetBool("isCorrectObjectIn", true);

            //tesla.SetActive(true);
            nameText.text = story.nameOfNpc;

            if (DragAnDrop.numberOfPartsIn < numberOfCorectParts - 1)
                sentence = story.sentences[index];
            else
                sentence = story.sentences[index] + " \nUspesno si zavrsio nivo.";

            StopAllCoroutines();

            StartCoroutine(TypeSentence(sentence));
            canvasGroup.interactable = false;
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
                canvasGroup.interactable = true;
            isContinueButtonEnabled = true;
            continueClick.enabled = true;
        }

        private void DestoryDDOLs()
        {
            if (time < 0)
            {
                GameObject[] ddols = GameObject.FindGameObjectsWithTag("DDOLs");
                GameObject inventory = GameObject.FindGameObjectWithTag("inventory");
                DontDestroyOnLoadManager.DestroyAll();
                foreach (GameObject ddol in ddols)
                    Destroy(ddol);

                Destroy(inventory);

                DragAnDrop.numberOfPartsIn = 0;
                ShowHint.isFirstTimeInScene = true;
            }
        }

        private void StartCounting()
        {
            if (startTimer)
                time -= Time.deltaTime;
        }

        private void ContinueButtonClick()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && isContinueButtonEnabled && continueClick.enabled)
            {
                if (DragAnDrop.numberOfPartsIn == 3)
                {
                    startTimer = true;
                    endScreenAnimator.Play("EndScreenFadeIn");
                }

                //dont let player cancel the text animation and leave room while tesla is done speaking

                continueClick.enabled = false;
                isCorrectObjectIn = false;
                imageAnimator.SetBool("isCorrectObjectIn", false);
                teslaAnimator.SetBool("isCorrectObjectIn", false);
            }
        }
    }
}