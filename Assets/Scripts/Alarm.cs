using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSignal;
    [SerializeField] private MotionSensor _motionSensor;

    private float _minSoundVolume = 0f;
    private float _maxSoundVolume = 1f;
    private float _strengthChangeVolume = 0.001f;
    private float _timeWaiting = 0.5f; 

    private void Start()
    {
        StartCoroutine(FadeIn());
        StartCoroutine(FadeAway());
    }

    public IEnumerator FadeIn()
    {
        while (_motionSensor.IsReached && _alarmSignal.volume != _maxSoundVolume)
        {
            ChangeSignal(_maxSoundVolume);
            yield return null;
        }

        yield return new WaitForSeconds(_timeWaiting);
        yield return FadeIn();
    }

    public IEnumerator FadeAway()
    {
        while (_motionSensor.IsReached == false && _alarmSignal.volume != _minSoundVolume)
        {
            ChangeSignal(_minSoundVolume);
            yield return null;
        }

        yield return new WaitForSeconds(_timeWaiting);
        yield return FadeAway();
    }

    private void ChangeSignal(float targetVolume)
    {
        _alarmSignal.volume = Mathf.MoveTowards(_alarmSignal.volume, targetVolume, _strengthChangeVolume);
    }
}