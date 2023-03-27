using UnityEngine;

public class ConversationTrigger : MonoBehaviour
{
    [SerializeField] private ConversationData data;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ConversationManager.Instance.StartConversation(data);
        }
    }
}
