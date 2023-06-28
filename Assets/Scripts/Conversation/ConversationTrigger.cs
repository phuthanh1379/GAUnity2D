using TMPro;
using UnityEngine;

public class ConversationTrigger : MonoBehaviour
{
    [SerializeField] private ConversationData data;
    [SerializeField] private string sentence;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject dialogueObject;

    private void Start()
    {
        dialogueObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //ConversationManager.Instance.StartConversation(data);
            Say(sentence);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dialogueObject.SetActive(false);
        }
    }

    private void Say(string toSay)
    {
        dialogueObject.SetActive(true);
        text.text = toSay;
    }
}
