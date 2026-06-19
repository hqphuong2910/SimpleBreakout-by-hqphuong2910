using Core;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Ball
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BallController : MyBehaviour
    {
        [Header("Movement Settings")] [SerializeField]
        private float moveSpeed = 5f;

        [SerializeField] private float xDirectionRange = 5f;

        [Header("References")] [SerializeField]
        private GameObject paddle;

        [SerializeField] private Vector2 offsetWithPaddle = new(0f, 0.5f);
        private bool _isLaunched;
        private Rigidbody2D _rb;

        private void Update()
        {
            if (_isLaunched) return;
            GetInputToLaunch();
            ConstraintPosition();
        }

        private void OnCollisionEnter2D()
        {
            ChangeDirection();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Death Zone"))
            {
                ResetBall();
                GameManager.Instance.UpdateLives(-1);
            }
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();

            LoadRigidbody2D();
        }

        private void LoadRigidbody2D()
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.gravityScale = 0;
            _rb.freezeRotation = true;
            _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            _rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }

        private void GetInputToLaunch()
        {
            if (InputManager.Instance.IsFired) LaunchBall();
        }

        private void ConstraintPosition()
        {
            if (!_isLaunched) transform.position = paddle.transform.position + (Vector3)offsetWithPaddle;
        }

        private void LaunchBall()
        {
            _isLaunched = true;
            var randomXDirection = Random.Range(-xDirectionRange, xDirectionRange);
            var launchDirection = new Vector2(randomXDirection, 1f).normalized;
            _rb.linearVelocity = launchDirection * moveSpeed;
        }

        private void ChangeDirection()
        {
            var velocity = _rb.linearVelocity;

            var isMovingTooStraight = (Mathf.Abs(velocity.x) < 0.2f) | (Mathf.Abs(velocity.y) < 0.2f);

            if (!isMovingTooStraight) return;
            var randomJitterX = Random.Range(-1f, 1f);
            var randomJitterY = Random.Range(-1f, 1f);
            var jitterDirection = new Vector2(randomJitterX, randomJitterY);
            var newDirection = (velocity + jitterDirection).normalized;
            _rb.linearVelocity = newDirection * moveSpeed;
        }

        private void ResetBall()
        {
            _isLaunched = false;
            _rb.linearVelocity = Vector2.zero;
            _rb.angularVelocity = 0f;
        }
    }
}