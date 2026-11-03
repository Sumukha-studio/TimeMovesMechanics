using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;
    private float _elapsedTime = 0f;


    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        int seconds = Mathf.FloorToInt(_elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((_elapsedTime * 100f) % 100f);

        _timeText.text = $"{seconds:00}.{milliseconds:00}";
    }

}