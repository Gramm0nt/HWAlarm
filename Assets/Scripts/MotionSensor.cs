using UnityEngine;
using UnityEngine.Events;

public class MotionSensor : MonoBehaviour
{
    [SerializeField] private UnityEvent<bool> _reached = new UnityEvent<bool>();

    public event UnityAction<bool> Reached
    {
        add => _reached.AddListener(value);
        remove => _reached.RemoveListener(value);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Thief>(out Thief thief))
        {
            _reached?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _reached?.Invoke(false);
    }
}