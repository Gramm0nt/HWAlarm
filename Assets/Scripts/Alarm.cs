using UnityEngine;
using UnityEngine.Events;

public class Alarm : MonoBehaviour
{
    [SerializeField] private UnityEvent _reached = new UnityEvent();
    [SerializeField] private AudioSource _alarmSignal;

    private float _minSoundVolume = 0f;
    private float _maxSoundVolume = 1f;
    private float _strengthChangeVolume = 0.3f;

    public event UnityAction Reached
    {
        add => _reached.AddListener(value);
        remove => _reached.RemoveListener(value);
    }

    public bool IsReached { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            IsReached = true;
            _reached?.Invoke();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        IsReached = false;
    }

    private void Update()
    {
        if (IsReached)
        {
            _alarmSignal.GetComponent<AudioSource>().volume = Mathf.MoveTowards(_alarmSignal.GetComponent<AudioSource>().volume,
                _maxSoundVolume, _strengthChangeVolume * Time.deltaTime);
        }

        else
        {
            _alarmSignal.GetComponent<AudioSource>().volume = Mathf.Clamp(_alarmSignal.GetComponent<AudioSource>().volume
                - _strengthChangeVolume * Time.deltaTime, _minSoundVolume, _maxSoundVolume);
        }
    }
}