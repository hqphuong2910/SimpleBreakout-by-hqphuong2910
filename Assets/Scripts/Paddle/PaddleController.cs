using System;
using Core;
using Managers;
using UnityEngine;

namespace Paddle
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PaddleController : MyBehaviour
    {
        [Header("Movement Settings")] [SerializeField]
        private float moveSpeed = 2f;

        [SerializeField] private float xRange = 1.8f;

        private float _horizontalInput;
        private Rigidbody2D _rb;

        private void Update()
        {
            _horizontalInput = InputManager.Instance.HorizontalMovement;
        }

        private void FixedUpdate()
        {
            _rb.linearVelocity = new Vector2(_horizontalInput * moveSpeed, _rb.linearVelocity.y);
        }

        private void LateUpdate()
        {
            var clampedPos = transform.position;
            clampedPos.x = Mathf.Clamp(clampedPos.x, -xRange, xRange);
            transform.position = clampedPos;
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
            _rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
            _rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            _rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }
}