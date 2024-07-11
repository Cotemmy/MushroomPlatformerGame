using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DialogueEditor;

public class ConversationStarter : MonoBehaviour
{
    [SerializeField] private NPCConversation myConversation;
    private bool conversationStarted = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!conversationStarted && other.CompareTag("Player"))
        {
            ConversationManager.Instance.StartConversation(myConversation);
            conversationStarted = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            conversationStarted = false;
        }
    }
}
