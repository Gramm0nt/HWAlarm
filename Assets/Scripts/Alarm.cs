using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSignal;
    [SerializeField] private MotionSensor _motionSensor;

    private float _minSoundVolume = 0f;
    private float _maxSoundVolume = 1f;
    private float _strengthChangeVolume = 0.001f;

    public IEnumerator FadeIn()
    {
        while (_motionSensor.IsReached)
        {
            _alarmSignal.volume = Mathf.MoveTowards(_alarmSignal.volume, _maxSoundVolume, _strengthChangeVolume);
            yield return null;
        }
    }

    public IEnumerator FadeAway()
    {
        while (_motionSensor.IsReached == false)
        {
            _alarmSignal.volume = Mathf.MoveTowards(_alarmSignal.volume, _minSoundVolume, _strengthChangeVolume);
            yield return null;
        }
    }
}