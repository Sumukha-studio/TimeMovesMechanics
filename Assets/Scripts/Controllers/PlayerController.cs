using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _playerSpeed = 5f;
    [SerializeField] private float _rotationSpeed = 10f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private float _timeTransitionSpeed = 1.0f;

    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _projectileSpawnPoint;

    private CharacterController _characterController;
    private Vector3 _velocity;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
        HandleShooting();
        HandleTimeScale();
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

    private void HandleShooting()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            Debug.Log("Space Key has been pressed");
            Shoot();
        }
    }

    private void Shoot()
    {
        if (_projectilePrefab != null && _projectileSpawnPoint != null)
        {
            Instantiate(_projectilePrefab, _projectileSpawnPoint.position, transform.rotation);
        }
    }

    private void HandleTimeScale()
    {
        bool _isMoving = false;

        if (Keyboard.current != null)
        {

            _isMoving = Keyboard.current.wKey.isPressed ||
                        Keyboard.current.sKey.isPressed ||
                        Keyboard.current.aKey.isPressed ||
                       Keyboard.current.dKey.isPressed;

        }

        if (_isMoving)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 0.1f, _timeTransitionSpeed * Time.unscaledDeltaTime);

        }
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
    }
}
