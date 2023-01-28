using UnityEngine;
using UnityEngine.Events;

public class MotionSensor : MonoBehaviour
{
    [SerializeField] private UnityEvent _reached = new UnityEvent();

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
}