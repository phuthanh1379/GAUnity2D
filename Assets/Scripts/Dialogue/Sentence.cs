using UnityEngine;

[CreateAssetMenu(fileName = "Sentence", menuName = "Dialogue/Sentence")]
public class Sentence : ScriptableObject
{
    public int id;
    public Character character;
    public string content;
    public int nextID;
}
