using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSignal;
    [SerializeField] private MotionSensor _motionSensor;

    private float _minSoundVolume = 0f;
    private float _maxSoundVolume = 1f;
    private float _strengthChangeVolume = 0.001f;

    public void SetVolume()
    {
        StartCoroutine(ChangeVolume());
    }

    private IEnumerator ChangeVolume()
    {
        while (_motionSensor.IsReached)
        {
            ChangeSignal(_maxSoundVolume);
            yield return null;
        }

        while (_motionSensor.IsReached == false)
        {
            ChangeSignal(_minSoundVolume);
            yield return null;
        }
    }

    private void ChangeSignal(float targetVolume)
    {
        _alarmSignal.volume = Mathf.MoveTowards(_alarmSignal.volume, targetVolume, _strengthChangeVolume);
    }
}