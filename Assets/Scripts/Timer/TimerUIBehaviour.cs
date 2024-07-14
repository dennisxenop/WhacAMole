using UnityEngine;
using Dennis.Variables;
using TMPro;

namespace Dennis.Timer
{
    public class TimerUIBehaviour : MonoBehaviour
    {
        [SerializeField]
        private FloatVariable timeLeft;

        [SerializeField]
        private TextMeshProUGUI timerText;

        private void OnEnable()
        {
            timeLeft.OnValueChanged -= timeLeftChanged;
            timeLeft.OnValueChanged += timeLeftChanged;
        }

        private void timeLeftChanged()
        {
            if (timeLeft.Value < 0) return;
            timerText.text = timeLeft.ToString();
        }

        private void Unsubscribe()
        {
            timeLeft.OnValueChanged -= timeLeftChanged;
        }

        private void OnDestroy()
        {
            Unsubscribe();
        }

        private void OnDisable()
        {
            Unsubscribe();
        }
    }
}