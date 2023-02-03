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
    private float _waitingTime = 0.3f;

    public void SetVolume()
    {

        if (_softVolumeChange != null)
        {
            StopCoroutine(_softVolumeChange);
        }

        _softVolumeChange = StartCoroutine(SetTargetVolume());
    }

    public void Update()
    {
        if (_motionSensor.IsReached)
        {
            _targetVolume = _maxSoundVolume;
        }

        else
        {
            _targetVolume = _minSoundVolume;
        }
    }

    private IEnumerator SetTargetVolume()
    {
        while (true)
        {
            while (_alarmSignal.volume != _targetVolume)
            {
                _alarmSignal.volume = Mathf.MoveTowards(_alarmSignal.volume, _targetVolume, _strengthChangeVolume);
                yield return null;
            }

            yield return new WaitForSeconds(_waitingTime);
        }
    }
}