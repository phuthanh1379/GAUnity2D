using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Conversation Data", menuName ="Conversation/Data")]
public class ConversationData : ScriptableObject
{
    public List<DialogSentence> data = new();
    //public List<string> sentences = new();
}

[Serializable]
public class DialogSentence
{
    public ConversationCharacter character;
    public CharacterAvatarPosition position;
    public string sentence;
    public List<string> choices;
}