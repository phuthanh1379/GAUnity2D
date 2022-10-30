using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Script")]
    [SerializeField] private CustomButtonBehaviour customButtonBehaviour;
    
    [Header("UI")]
    [SerializeField] private Button aboutButton;
    [SerializeField] private TMP_Text enterDisplayLabel;
    [SerializeField] private TMP_Text exitDisplayLabel;
    
    private int _enterCount = 0;
    private int _exitCount = 0;

    private void OnEnable()
    {
        aboutButton.onClick.AddListener(OnClickButton);
        customButtonBehaviour.OnCustomMouseEnterEvent += OnCustomMouseEnter;
        customButtonBehaviour.OnCustomMouseExitEvent += OnCustomMouseExit;
    }

    private void OnDisable()
    {
        aboutButton.onClick.RemoveListener(OnClickButton);
        customButtonBehaviour.OnCustomMouseEnterEvent -= OnCustomMouseEnter;
        customButtonBehaviour.OnCustomMouseExitEvent -= OnCustomMouseExit;
    }

    private void OnCustomMouseExit()
    {
        _exitCount++;
        exitDisplayLabel.text = $"Exit Count: {_exitCount}";
    }

    private void OnCustomMouseEnter()
    {
        _enterCount++;
        enterDisplayLabel.text = $"Enter Count: {_enterCount}";
    }

    private void OnDestroy()
    {
        aboutButton.onClick.RemoveListener(OnClickButton);
    }

    public void OnClickButton()
    {
        Debug.Log("Clicked!");
    }
}
