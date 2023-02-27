using System;
using Core.Enums;
using Core.Tools;
using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        public bool IsDisabled = false;

        [Header("HorizontalMovement")]
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private Direction _direction;

        [Header("Jump")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _gravityScale;
        [SerializeField] private bool _isGrounded;

        [SerializeField] private DirectionalCameraPair _cameras;

        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void MoveHorizontaly(float direction)
        {
            if (IsDisabled)
                return;

            SetDirection(direction);

            Vector2 velocity = _rigidbody.velocity;
            velocity.x = direction * _horizontalSpeed;

            _rigidbody.velocity = velocity;
            _rigidbody.gravityScale = _gravityScale;
        }

        public void Jump()
        {
            if (IsDisabled)
                return;

            if (!_isGrounded)
                return;

            _rigidbody.AddForce(Vector2.up * _jumpForce);
            _rigidbody.gravityScale = _gravityScale;
            _isGrounded = false;
        }

        private void SetDirection(float direction)
        {
            if (IsDisabled)
                return;

            if ((_direction == Direction.Right && direction < 0) || 
                (_direction == Direction.Left && direction > 0))
            {
                Flip();
            }
        }

        private void Flip()
        {
            if (IsDisabled)
                return;

            transform.Rotate(0, 180, 0);
            _direction = _direction == Direction.Right ? Direction.Left : Direction.Right;

            foreach (var cameraPair in _cameras.DirectionalCameras)
                cameraPair.Value.enabled = cameraPair.Key == _direction;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (IsDisabled)
                return;

            if (other.gameObject.CompareTag("Ground") && _isGrounded == false)
            {
                _isGrounded = true;
            }
        }
    }
}