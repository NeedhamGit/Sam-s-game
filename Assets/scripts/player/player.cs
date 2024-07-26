using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private CharacterController _characterController;
    [SerializeField] private float _movespeed;
    [SerializeField] private float _gravity;
    [SerializeField] private float _jumpHeight;
    private Vector3 _startPosition;
    private float _yVelocity;
    int coinCount = 0;
    bool doubleJump = false;
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

    void OnTriggerEnter(Collider collision)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (collision.gameObject.tag.ToString() == "Enemy")
        {
            print(transform.position.y > (collision.gameObject.transform.position.y + 1));
            if (transform.position.y > (collision.gameObject.transform.position.y+.5)) {

                _yVelocity = _jumpHeight;
                Destroy(collision.gameObject);
                
            } else
            {
                resetPlayer();
            }
        }
        if (collision.gameObject.tag.ToString() == "coin")
        {
            if (!collision.gameObject.GetComponent<AudioSource>().isPlaying)
            {
                coinCount++;
                collision.gameObject.GetComponent<AudioSource>().Play();
                _startPosition = transform.position;
                GameObject.Find("coin UI").GetComponent<TextMeshProUGUI>().text = "Coins:"+coinCount.ToString();
            }
            

        }
    }

    void PlayerMovement()
    {
         float horizontalSpeed = 2.0F;
         float verticalSpeed = 2.0F;
        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * Input.GetAxis("Mouse Y");
        transform.Rotate(0,h,0);
        float _horizontalInput = Input.GetAxis("Horizontal");
        float _verticalInput = Input.GetAxis("Vertical");
        Vector3 _moveDirection = new Vector3(_horizontalInput, 0, _verticalInput);
        Vector3 _playerVelocity = transform.rotation*_moveDirection.normalized * _movespeed;


        //Right mouse button makes the cursor lock
        if (Input.GetMouseButtonDown(1))
        {
           if( Cursor.lockState == CursorLockMode.Locked)
            Cursor.lockState = CursorLockMode.None;
            else if( Cursor.lockState == CursorLockMode.None)
            Cursor.lockState = CursorLockMode.Locked;
        }

        //double jump code for andrew
        if (Input.GetButtonDown("Jump") && doubleJump)
        {
            _yVelocity = _jumpHeight;
            doubleJump = false;
        }
        if (_characterController.isGrounded || Physics.Raycast(transform.position, -Vector3.up, 1.5f))
        {  
            if(_yVelocity < 0 && Physics.Raycast(transform.position, -Vector3.up, 1.1f))
            {
                _yVelocity = -_gravity;
            }

            if (Input.GetButtonDown("Jump"))
            {
                _yVelocity = _jumpHeight;
                doubleJump = true;
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
            resetPlayer();
        }
    }
    void resetPlayer()
    {
        print("reset");
        transform.position = _startPosition;
        _yVelocity = 0;

    }    

}
