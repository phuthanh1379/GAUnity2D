using TMPro;
using UnityEngine;
using Random = System.Random;

public class ScriptingManager : MonoBehaviour
{
    [SerializeField] private GameObject myButtonPrefab;
    [SerializeField] private Transform prefabParent;
    [SerializeField] private TMP_Text scoreLabel;
    [SerializeField] private float rate;

    private const int MaxX = 910;
    private const int MaxY = 420;
    private const float Difficulty = 0.6f;
    private const float MINRate = 1.5f;
    
    private Random _rnd;
    private float _timer = 0;
    private int _counter = 0;
    

    private void Awake()
    {
        _rnd = new Random();
    }

    private void Update()
    {
        if (_timer >= rate)
        {
            SpawnPrefab(myButtonPrefab, prefabParent);
            _timer = 0;
        }
        else
        {
            _timer += Time.deltaTime;
        }
    }

    private void SpawnPrefab(GameObject prefab, Transform parent)
    {
        var go = Instantiate(prefab, parent);
        var x = _rnd.Next(-MaxX, MaxX);
        var y = _rnd.Next(-MaxY, MaxY);
        
        go.transform.localPosition = new Vector2(x, y);

        go.GetComponent<MyButtonBehaviour>().OnMyButtonClicked += OnClickMyButton;
    }

    private void OnClickMyButton()
    {
        _counter++;
        if (rate > MINRate)
        {
            rate -= Difficulty;
        }
        scoreLabel.text = _counter.ToString();
    }
}
