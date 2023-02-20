using Unity.VisualScripting;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        [Header("HorizontalMovement")]
        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private bool _faceRight;

        [Header("Jump")]
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _gravityScale;
        [SerializeField] private bool _isGrounded;

        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public void MoveHorizontaly(float direction)
        {
            SetDirection(direction);

            Vector2 velocity = _rigidbody.velocity;
            velocity.x = direction * _horizontalSpeed;

            _rigidbody.velocity = velocity;
        }

        public void Jump()
        {
            if (!_isGrounded)
                return;

            _rigidbody.AddForce(Vector2.up * _jumpForce);
            _rigidbody.gravityScale = _gravityScale;
            _isGrounded = false;
        }

        private void SetDirection(float direction)
        {
            if ((_faceRight && direction < 0) || 
                (!_faceRight && direction > 0))
            {
                Flip();
            }
        }

        private void Flip()
        {
            transform.Rotate(0, 180, 0);
            _faceRight = !_faceRight;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Ground") && _isGrounded == false)
            {
                _isGrounded = true;
            }
        }
    }
}