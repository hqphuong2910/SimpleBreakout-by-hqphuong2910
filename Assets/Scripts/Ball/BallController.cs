using Core;
using Managers;
using UnityEngine;

namespace Ball
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class BallController : MyBehaviour
    {
        [Header("Movement Settings")] [SerializeField]
        private float moveSpeed = 5f;

        [SerializeField] private float xDirectionRange = 5f;
        [SerializeField] private float bottomBound = -5.2f;

        [Header("References")] [SerializeField]
        private GameObject paddle;

        [SerializeField] private Vector2 offsetWithPaddle = new(0f, 0.5f);
        private bool _isLaunched;
        private Rigidbody2D _rb;

        private void Update()
        {
            DisableOffScreen();
            if (_isLaunched) return;
            GetInputToLaunch();
            ConstraintPosition();
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

        private void DisableOffScreen()
        {
            if (transform.position.y <= bottomBound)
            {
                gameObject.SetActive(false);
            }
        }
    }
}