using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private CharacterController _characterController;
    [SerializeField] private float _movespeed = 2f;
    [SerializeField] private float _gravity = .6f;
    [SerializeField] private float _jumpHeight = 30f;
    private Vector3 _startPosition;
    private float _yVelocity;
    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
        PlayerFall();
    }

    void PlayerMovement()
    {
        float _horizontalInput = Input.GetAxis("Horizontal");
        Vector3 _moveDirection = new Vector3(_horizontalInput, 0, 0);
        Vector3 _playerVelocity =  _moveDirection * _movespeed;

        if (Physics.Raycast(transform.position, -Vector3.up, 1.1f))
        {  
            if(_yVelocity < 0)
            {
                _yVelocity = -_gravity;
            }

            if (Input.GetButtonDown("Jump"))
            {
                _yVelocity = _jumpHeight;
            }
        } else if(_playerVelocity.y > -3f)
        {
            _yVelocity -= _gravity;
        }
        _playerVelocity.y = _yVelocity;
        _characterController.Move(_playerVelocity * Time.deltaTime);
    }

    void PlayerFall()
    {
        if(transform.position.y <-5f) {
            transform.position = _startPosition;
            _yVelocity = 0;
        }
    }
}
