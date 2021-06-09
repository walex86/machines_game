using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LevelElement : MonoBehaviour
{
    public bool TargetMovement;
    public bool Target;
    public bool TutorialElement { get; private set; }
    
    private event Action _onMouseDown;
    private event Action _onMouseUp;
    private event Action _complete;
    private bool _drag;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _spriteRenderer;

    public void Init(Action onMouseDown, Action onMouseUp, bool tutorialElement = false, Action onComplete = null)
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _onMouseDown = onMouseDown;
        _onMouseUp = onMouseUp;
        _complete = onComplete;
        if (tutorialElement)
        {
            var col = _spriteRenderer.color;
            col.a = 0.4f;
            _spriteRenderer.color = col;
            TutorialElement = true;
        }
    }

    private void OnMouseDown()
    {
       _onMouseDown?.Invoke(); 
    }

    private void OnMouseUp()
    {
        _onMouseUp?.Invoke();
    }

    public void StartDrag()
    {
        _drag = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (TargetMovement)
        {
            if (other.gameObject.GetComponent<LevelElement>().Target)
            {
                _complete?.Invoke();
            }
        }
    }

    private void Update()
    {
        if (!_drag) return;
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0;
        transform.position = pos;
    }

    public void StopDrag()
    {
        _drag = false;
    }

    public void Freeze(bool enable)
    {
        _rigidbody.constraints = enable ? RigidbodyConstraints2D.FreezeAll : RigidbodyConstraints2D.None;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.angularVelocity = 0;
    }
}
