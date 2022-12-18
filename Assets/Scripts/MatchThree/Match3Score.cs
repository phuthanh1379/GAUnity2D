using TMPro;
using UnityEngine;

public class Match3Score : MonoBehaviour
{
    public static Match3Score Instance;
    public TMP_Text scoreLabel;

    private int _score;
    public int Score
    {
        get => _score;
        set
        {
            if (_score == value) return;

            _score = value;

            scoreLabel.text = $"Score: {_score}";
        }
    }
    
    private void Awake()
    {
        Instance = this;
    }
}
