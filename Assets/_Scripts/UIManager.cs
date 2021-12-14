using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] Slider sliderPlayerHP;
    [SerializeField] TextMeshProUGUI mainTaxt;

    Animator animator;

    static int AnimShowText = Animator.StringToHash("Text");

    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
    }

    public void SetupSliderPlayerHP(int value)
    {
        sliderPlayerHP.maxValue = value;
        sliderPlayerHP.value = value;
    }

    public void SetSliderPlayerHP(int value)
    {
        sliderPlayerHP.value = value;
    }

    public void SetShowText(string message)
    {
        mainTaxt.gameObject.SetActive(true);
        mainTaxt.text = message;
        animator.SetTrigger(AnimShowText);
    }
}
