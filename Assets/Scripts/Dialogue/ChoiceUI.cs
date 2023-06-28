using System;
using TMPro;
using UnityEngine;

public class ChoiceUI : MonoBehaviour
{
    public static event Action<int> GoToDialogue;

    [SerializeField] private TMP_Text content;

    private int _nextID;

    public void Setup(Choice choice)
    {
        content.text = choice.content;
        _nextID = choice.nextID;
    }

    public void ClickChoice()
    {
        GoToDialogue?.Invoke(_nextID);
    }
}
