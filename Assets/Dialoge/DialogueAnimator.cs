using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueAnimator : MonoBehaviour
{
    public Animator boxAnim;
    public DialogueManager dm;

    public void OnTriggerEnter2D(Collider2D other)
    {
        boxAnim.SetBool("dialogueOpen", true);
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        boxAnim.SetBool("dialogueOpen", false);
        dm.EndDialogue();
    }
}
