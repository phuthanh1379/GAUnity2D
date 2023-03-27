using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Conversation/Character")]
public class ConversationCharacter : ScriptableObject
{
    public new string name;
    public Sprite sprite;
}

public enum CharacterAvatarPosition
{
    Left = 0,
    Right = 1
}
