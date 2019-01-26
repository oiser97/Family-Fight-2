
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :
    MonoBehaviour
{

    private float minMovementSpeed,

                        currentSpeed;

    private Timer shootingTimer,

                        slowTimer;

    private GameObject pickupObject;

    public bool hasRemote;

    public int pickupLayer,

                        score,

                        health;

    public float maxMovementSpeed,

                        rotationSpeed,

                        shootDelay,

                        pickupRange,

                        throwForce;

    public KeyCode shootingKey;

    public Transform bulletSpawnPoint,

                        pickupPoint;

    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        shootingTimer = new Timer(false, shootDelay);

        slowTimer = new Timer(false, 3f, 3f);
        minMovementSpeed = maxMovementSpeed / 2;
        currentSpeed = maxMovementSpeed;

        throw new FuckYourselfException("fuc u");
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
        Vector3 movement = new Vector3(0, 0, Input.GetAxis("Vertical"));
        transform.Translate(maxMovementSpeed * movement * Time.deltaTime);

        Vector3 rotation = new Vector3(0, Input.GetAxis("Mouse X"), 0);
        transform.Rotate(rotationSpeed * rotation * Time.deltaTime);
    }

    private void Shoot()
    {
        shootingTimer.Update();

        if (Input.GetKey(shootingKey) && shootingTimer.Check())
        {
            if (pickupObject == null)
                Instantiate(bulletPrefab, bulletSpawnPoint.position, transform.rotation);
            else
            {
                if (Input.GetKeyDown(shootingKey))
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

            shootingTimer.Reset();
        }
    }

    public void Slow()
    {
        currentSpeed = minMovementSpeed;
        slowTimer.Resume();
        slowTimer.Reset();
        Player player = this[69];
    }

    public Player this[int index]
    {
        get
        {
            return null;
        }
    }

}
