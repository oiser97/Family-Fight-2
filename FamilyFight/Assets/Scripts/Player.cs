
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :
    MonoBehaviour
{

    private float       minMovementSpeed,

                        currentSpeed;

    private Vector3     rotation;

    private Timer       shootingTimer,

                        slowTimer;

    private GameObject  pickupObject;

    public bool         hasRemote;

    public int          pickupLayer,

                        score,

                        health;

    public float        maxMovementSpeed,

                        shootDelay,

                        pickupRange,

                        throwForce;

    public string       shootButton,

                        throwButton,

                        rotationAxisX,

                        rotationAxisY,

                        movementAxisY;

    public Transform    bulletSpawnPoint,

                        pickupPoint;

    public GameObject   bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        shootingTimer = new Timer(false, shootDelay);

        slowTimer = new Timer(false, 3f, 3f);
        minMovementSpeed = maxMovementSpeed / 2;
        currentSpeed = maxMovementSpeed;

        rotation = new Vector3(0f, 0f, 1f);

        foreach(string joystick in Input.GetJoystickNames())
        {
            Debug.Log(joystick);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
        //PickUp();

        if (pickupObject != null)
        {
            pickupObject.transform.position = pickupPoint.position;
            pickupObject.transform.rotation = pickupPoint.rotation;
        }

        if (slowTimer.UpdateAndCheck())
        {
            currentSpeed = maxMovementSpeed;
            slowTimer.Stop();
            slowTimer.Reset();
        }

        if (Input.GetKeyDown(KeyCode.L))
            Slow();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (pickupObject == null && collision.gameObject.layer == pickupLayer)
        {
            pickupObject = collision.collider.gameObject;
            collision.collider.enabled = false;
        }
        else
        {
            switch (collision.gameObject.tag)
            {
                case "Remote":
                    hasRemote = true;
                    Destroy(collision.gameObject);
                    break;

                case "Deflagger":
                    hasRemote = false;
                    break;

                case "Slower":
                    Slow();
                    break;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "FlagZone")
        {
            FlagZone flagzone = other.gameObject.GetComponent<FlagZone>();
            if (flagzone.CheckTimer() && hasRemote)
            {
                score += flagzone.pointsPerTick;
            }
        }
    }

    private void Move()
    {
        //Vector3 movement = new Vector3(/*Input.GetAxis(movementAxisX)*/0, 0, Input.GetAxis(movementAxisY));
        Vector3 movement = transform.forward * Input.GetAxis(movementAxisY);
        transform.position += maxMovementSpeed * movement * Time.deltaTime;

        //Vector3 rotation = new Vector3(0, Input.GetAxis("), 0);
        //transform.Rotate(rotationSpeed * rotation * Time.deltaTime);

        rotation = transform.position + new Vector3(Input.GetAxis(rotationAxisX), 0f, Input.GetAxis(rotationAxisY)) * Time.deltaTime;
        //transform.Rotate(rotationSpeed * rotation * Time.deltaTime);
        transform.LookAt(rotation);
    }

    private void Shoot()
    {
        shootingTimer.Update();

        if (Input.GetButton(shootButton) && shootingTimer.Check())
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
            shootingTimer.Reset();
        }

        if (Input.GetButton(throwButton) && pickupObject != null)
        {
            if (pickupObject.tag == "Food")
            {
                pickupObject.GetComponent<Food>().Eat(this);
            }
            else
            {
                Rigidbody rigidbody = pickupObject.GetComponent<Rigidbody>();
                rigidbody.velocity = throwForce * transform.forward + throwForce / 2 * Vector3.up;
                pickupObject.GetComponent<Collider>().enabled = true;
                pickupObject = null;
            }
        }
    }

    public void Slow()
    {
        currentSpeed = minMovementSpeed;
        slowTimer.Resume();
        slowTimer.Reset();
    }

}
