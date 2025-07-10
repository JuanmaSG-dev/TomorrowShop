using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    TMP_Text timerText;
    public float remainingTime = 60f;

    private void Start()
    {
        timerText = GetComponent<TMP_Text>();
        timerText.text = "00:00";
    }

    void Update()
    {
        AdvanceTimer();
    }

    private void AdvanceTimer()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            int minutes = Mathf.FloorToInt(remainingTime / 60f);
            int seconds = Mathf.FloorToInt(remainingTime % 60f);

            minutes = minutes < 0 ? 0 : minutes;
            seconds = seconds < 0 ? 0 : seconds;

            if (minutes == 0 && seconds == 0)
            {
                timerText.text = "00:00";
                timerText.overflowMode = TextOverflowModes.Overflow;
                timerText.fontSize = 50;
                timerText.color = Color.red;
            }

            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}
