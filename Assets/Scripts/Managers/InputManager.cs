using Core;

namespace Managers
{
    public class InputManager : Singleton<InputManager>
    {
        private InputSystem_Actions _inputActions;
        public float HorizontalMovement { get; private set; }
        public bool IsFired { get; private set; }

        private void Update()
        {
            ReadInput();
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();

            LoadInputActions();
        }

        protected override void SubscribeEvents()
        {
            base.SubscribeEvents();

            _inputActions.Enable();
        }

        protected override void UnsubscribeEvents()
        {
            base.UnsubscribeEvents();

            _inputActions.Disable();
        }

        private void LoadInputActions()
        {
            if (_inputActions != null) return;
            _inputActions = new InputSystem_Actions();
        }

        private void ReadInput()
        {
            ReadMovementInput();
            ReadFireInput();
        }

        private void ReadMovementInput()
        {
            HorizontalMovement = _inputActions.Player.Move.ReadValue<float>();
        }

        private void ReadFireInput()
        {
            IsFired = _inputActions.Player.Fire.WasPressedThisFrame();
        }
    }
}