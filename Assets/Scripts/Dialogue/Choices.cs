using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Choices", menuName = "Dialogue/Choices")]
public class Choices : ScriptableObject
{
    public int id;
    public List<Choice> choicesList = new();
}
