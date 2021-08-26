using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float MoveSpeed = 3;
    public float TurnSpeed = 5;
    public float JumpPower = 3;
    private Rigidbody _rbody;
    private Transform _camera;
    private float _cameraAngle;
    // Start is called before the first frame update
    void Start()
    {
        _rbody = GetComponent<Rigidbody>();
        _camera = GetComponentInChildren<Camera>().transform;
        _cameraAngle = _camera.rotation.x;
    }

    // Update is called once per frame
    private bool _jumpCommand = false;
    void Update()
    {
        var mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, mouseX * TurnSpeed, 0);
        if (Input.GetButtonDown("Jump"))
        {
            _jumpCommand = true;
        }
        var mouseY = Input.GetAxis("Mouse Y");
        _cameraAngle = _cameraAngle - mouseY * TurnSpeed;
        _camera.localRotation = Quaternion.Euler(_cameraAngle, 0, 0);
    }
    bool CanJump = true;
    private void FixedUpdate()
    {
        var direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        direction = transform.rotation * Vector3.ClampMagnitude(direction, 1);
        direction *= MoveSpeed;
        if (_jumpCommand && CanJump)
        {
            _jumpCommand = false;
            direction.y = JumpPower;
        }
        else
        {
            direction.y = _rbody.velocity.y;
        }
        _rbody.velocity = direction;
    }
    public void GetResource(string resourceName)
    {
        foreach(var resource in GetComponentsInChildren<Resource>())
        {
            if (resource.ResourceName == resourceName)
            {
                resource.Amount += 1;
                break;
            }
        }
    }
    public bool CheckResource(string resourceName, int amount = 1, bool remove = false)
    {
        foreach(var resource in GetComponentsInChildren<Resource>())
        {
            if (resource.ResourceName == resourceName)
            {
                if (resource.Amount >= amount)
                {
                    if (remove)
                    {
                        resource.Amount -= amount;
                    }
                    return true;
                }
                else return false;
            }
        }
        return false;
    }

}
