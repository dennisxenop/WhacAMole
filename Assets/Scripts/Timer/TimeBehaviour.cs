using Dennis.Variables;
using System.Collections;
using UnityEngine;

public class TimeBehaviour : MonoBehaviour
{
    [SerializeField]
    private BoolVariable roundRunning;

    [SerializeField]
    private FloatVariable timeTotal;

    [SerializeField]
    private FloatVariable timeLeft;

    private Coroutine countdownCoroutine;

    private float currentTime;
    private int lastTimeInSeconds;
    private void OnEnable()
    {
        roundRunning.OnValueChanged -= roundRunningChanged;
        roundRunning.OnValueChanged += roundRunningChanged;
    }

    private void Start()
    {
        timeLeft.Value = timeTotal.Value;
        currentTime = timeTotal.Value;
        lastTimeInSeconds = Mathf.CeilToInt(timeTotal.Value);
    }

    private void roundRunningChanged(bool isRunning)
    {
        StopCountdown();

        if (!isRunning || timeLeft.Value <= 0)
        {
            return;
        }

        StartCountdown();
    }

    private void StopCountdown()
    {
        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }
    }

    private void StartCountdown()
    {
        countdownCoroutine = StartCoroutine(CountdownRoutine());
    }

    private IEnumerator CountdownRoutine()
    {
        while (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            int roundedTime = Mathf.CeilToInt(currentTime);

            if (roundedTime != lastTimeInSeconds)
            {
                lastTimeInSeconds = roundedTime;
                timeLeft.Value = lastTimeInSeconds;
            }
            yield return null;
        }
        timeLeft.Value = 0;
    }

    private void Unsubscribe()
    {
        roundRunning.OnValueChanged -= roundRunningChanged;
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
