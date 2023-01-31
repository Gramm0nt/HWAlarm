using UnityEngine;
using UnityEngine.Events;

public class MotionSensor : MonoBehaviour
{
    [SerializeField] private UnityEvent _reached = new UnityEvent();
    [SerializeField] private Alarm _alarm;

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
            StopCoroutine(_alarm.FadeAway());
            StartCoroutine(_alarm.FadeIn());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsReached = false;
        StopCoroutine(_alarm.FadeIn());
        StartCoroutine(_alarm.FadeAway());
    }
}