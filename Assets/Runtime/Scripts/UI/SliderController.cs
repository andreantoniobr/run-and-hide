using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    [SerializeField] private Slider slider;

    private void Awake()
    {
        ScoreManager.UpdateScoreEvent += UpdateSlider;
    }

    private void OnDestroy()
    {
        ScoreManager.UpdateScoreEvent -= UpdateSlider;
    }

    private void UpdateSlider(float currentScore)
    {
        if (slider)
        {
            slider.value = currentScore;
        }
    }
}
