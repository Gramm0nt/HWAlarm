using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSignal;
    [SerializeField] private MotionSensor _motionSensor;

    private float _minSoundVolume = 0f;
    private float _maxSoundVolume = 1f;
    private float _strengthChangeVolume = 0.001f;
    private float _countRepeats = 10;

    private void Update()
    {
        if (_motionSensor.IsReached)
        {
            StopCoroutine(FadeAway());
            StartCoroutine(FadeIn());
        }

        else
        {
            StopCoroutine(FadeIn());
            StartCoroutine(FadeAway());
        }
    }

    private IEnumerator FadeIn()
    {
        for (int index = 0; index < _countRepeats; index++)
        {
            _alarmSignal.volume = Mathf.MoveTowards(_alarmSignal.volume, _maxSoundVolume, _strengthChangeVolume);
            yield return null;
        }
    }

    private IEnumerator FadeAway()
    {
        for (int index = 0; index < _countRepeats; index++)
        {
            _alarmSignal.volume = Mathf.MoveTowards(_alarmSignal.volume, _minSoundVolume, _strengthChangeVolume);
            yield return null;
        }
    }
}