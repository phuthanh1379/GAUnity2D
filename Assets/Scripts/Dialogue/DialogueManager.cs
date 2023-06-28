using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private List<Sentence> sentencesData = new();
    [SerializeField] private List<Choices> choicesData = new();

    [SerializeField] private TMP_Text characterNameLabel;
    [SerializeField] private TMP_Text contentLabel;
    [SerializeField] private ChoiceUI choicePrefab;
    [SerializeField] private Transform choicesParent;

    private void OnEnable()
    {
        ChoiceUI.GoToDialogue += OnGoToDialogue;
    }

    private void OnDisable()
    {
        ChoiceUI.GoToDialogue -= OnGoToDialogue;
    }

    private void Start()
    {
        if (sentencesData == null) return;
        if (sentencesData.Count <= 0) return;
        DisplaySentence(sentencesData[0]);
    }

    private void DisplaySentence(Sentence sentence)
    {
        characterNameLabel.text = sentence.character.name;
        contentLabel.text = sentence.content;

        // Đồng thời cũng hiển thị lựa chọn cho người chơi
        foreach (Transform child in choicesParent)
        {
            Destroy(child.gameObject);
        }

        if (choicesData == null) return;
        if (choicesData.Count <= 0) return;

        foreach (var choices in choicesData)
        {
            if (choices.id == sentence.nextID)
            {
                foreach(var choice in choices.choicesList)
                {
                    DisplayChoice(choice);
                }
            }
        }
    }

    private void DisplayChoice(Choice choice)
    {
        var choiceObject = Instantiate(choicePrefab, choicesParent);
        choiceObject.Setup(choice);
    }

    private void OnGoToDialogue(int id)
    {
        if (sentencesData == null) return;
        if (sentencesData.Count <= 0) return;

        foreach (var sentence in sentencesData)
        {
            if (sentence.id == id)
                DisplaySentence(sentence);
        }
    }
}
