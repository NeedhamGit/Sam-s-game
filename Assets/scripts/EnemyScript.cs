using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private CharacterController _characterController;
    public GameObject target;
    [SerializeField] private float _gravity = .6f;
    private float speed = 7.0f;
    public float _yVelocity;

    public GameObject coin;
    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        target = GameObject.Find("It's Mi'a, Mario!");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //print(Physics.Raycast(transform.position, -Vector3.up, 1.5f));
        if (_characterController.isGrounded || Physics.Raycast(transform.position, -Vector3.up, 1.5f))
        {
            if (_yVelocity < 0)
            {
                _yVelocity = _gravity;
            }
        } else if(_yVelocity > -3f)
        {
            _yVelocity -= _gravity;
        }
            var dist = Vector3.Distance(transform.position, target.transform.position);
        var movevec = new Vector3();
        if(dist < 20)
        {
            var look = target.transform.position;
            look.y = transform.position.y;
            transform.LookAt(look);
            movevec = (target.transform.position - transform.position).normalized * speed;
        }
        movevec.y = _yVelocity;
        _characterController.Move(movevec * Time.deltaTime);
    }

    private void OnDestroy()
    {
        print("coinSpawn");
        Instantiate(coin, transform.position, Quaternion.Euler(0f,0f,90f));
    }
}
