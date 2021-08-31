using UnityEngine;

namespace FourGear.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        public Story story;

        public void TriggerDialogue(int index)
        {
            if (TimerManager.timeValue > 0)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(story, index);
            }
        }
    }
}
