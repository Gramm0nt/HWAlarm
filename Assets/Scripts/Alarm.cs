using System.Collections;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    [SerializeField] private AudioSource _alarmSignal;
    [SerializeField] private MotionSensor _motionSensor;

    private Coroutine _softVolumeChange;
    private float _minSoundVolume = 0f;
    private float _maxSoundVolume = 1f;
    private float _strengthChangeVolume = 0.001f;
    private float _targetVolume;

    private void SetVolume(bool IsReached)
    {
        if (_softVolumeChange != null)
        {
            StopCoroutine(_softVolumeChange);
        }
        
        if (IsReached)
        {
            _targetVolume = _maxSoundVolume;
        }

        else
        {
            _targetVolume = _minSoundVolume;
        }

        _softVolumeChange = StartCoroutine(SetTargetVolume());
    }

    private void OnEnable()
    {
        _motionSensor.Reached += SetVolume;
    }

    private void OnDisable()
    {
        _motionSensor.Reached -= SetVolume;
    }

    private IEnumerator SetTargetVolume()
    {
        while (_alarmSignal.volume != _targetVolume)
        {
            _alarmSignal.volume = Mathf.MoveTowards(_alarmSignal.volume, _targetVolume, _strengthChangeVolume);
            yield return null;
        }
    }
}