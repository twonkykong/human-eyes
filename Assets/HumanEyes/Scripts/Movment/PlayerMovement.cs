using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace HumanEyes.Scripts.Movment
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {
        private InputMaster _inputMaster;
        private InputAction _moveInput;

        [SerializeField] private float gravity = 9.81f;
        [SerializeField] private float speed = 10f;

        private CharacterController _characterController;
        private Transform _thisObjectTransform;

        private void Awake()
        {
            _inputMaster = new InputMaster();
            _moveInput = _inputMaster.Actions.Move;

            _characterController = GetComponent<CharacterController>();
            _thisObjectTransform = transform;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            Vector2 moveInput = _moveInput.ReadValue<Vector2>();
            Vector3 moveDirection = (_thisObjectTransform.right * moveInput.x + _thisObjectTransform.forward *  moveInput.y) * speed;

            if (!_characterController.isGrounded)
            {
                moveDirection /= 2;
                moveDirection -= Vector3.up * gravity;
            }

            _characterController.Move(moveDirection * Time.deltaTime);
        }

        private void OnEnable()
        {
            _inputMaster.Enable();
        }

        private void OnDisable()
        {
            _inputMaster.Disable();
        }
    }

}

