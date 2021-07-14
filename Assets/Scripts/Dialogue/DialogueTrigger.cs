using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FourGear.Dialogue
{
    public class DialogueTrigger : MonoBehaviour
    {
        public Story story;

        public void TriggerDialogue()
        {
            FindObjectOfType<DialogueManager>().StartDialogue(story);
        }
    }
}
