using UnityEngine;
using Dennis.Variables;
using TMPro;
using UnityEngine.Assertions;

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
            Assert.IsNotNull(timeLeft, "timeLeft is not found");
            Assert.IsNotNull(timerText, "timerText is not found");

            timeLeft.OnValueChanged -= timeLeftChanged;
            timeLeft.OnValueChanged += timeLeftChanged;
        }

        private void timeLeftChanged()
        {
            if (timeLeft.Value < 0) return;
            timerText.text = timeLeft.Value.ToString();
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