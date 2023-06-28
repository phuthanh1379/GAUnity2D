using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField] private TestSO data;
    [SerializeField] private Slider slider;

    private void Start()
    {
        if (data == null) return;
        slider.value = data.value;
    }

    public void ClickLoadScene()
    {
        SceneManager.LoadScene("Test2");
    }

    public void OnSliderValueChanged()
    {
        data.value = slider.value;
    }
}
