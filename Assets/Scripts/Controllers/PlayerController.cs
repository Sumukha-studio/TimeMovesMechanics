using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _gravity = -9.81f;

    private CharacterController _characterController;
    private Vector3 _velocity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
    }


    private void HandleMovement()
    {
        if (_characterController.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        float forwardInput = 0f;
        float turnInput = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.wKey.isPressed) forwardInput += 1f;
            if (Keyboard.current.sKey.isPressed) forwardInput -= 1f;
            if (Keyboard.current.aKey.isPressed) turnInput -= 1f;
            if (Keyboard.current.dKey.isPressed) turnInput += 1f;
        }

        transform.Rotate(0f, turnInput * _rotationSpeed * Time.deltaTime, 0f);

        Vector3 move = transform.forward * forwardInput;
        _velocity.y += _gravity * Time.deltaTime;

        Vector3 totalMovement = (move * _playerSpeed + _velocity) * Time.deltaTime;
        _characterController.Move(totalMovement);
    }

}