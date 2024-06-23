using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public class ExpSlider : MonoBehaviour
    {
        public Slider slider;
        public TextMeshProUGUI text;

        public void UpdateSlider(int exp, int maxExp)
        {
            slider.value = (float)exp / maxExp;
            text.text = $"{exp} / {maxExp}";
        }
    }
}