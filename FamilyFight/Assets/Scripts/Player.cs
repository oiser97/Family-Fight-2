
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player :
    MonoBehaviour
{

    // Movement speed.

    private float       minMovementSpeed,

                        currentSpeed;

    // Player rotation.

    private Vector3     rotation;

    // Timers.

    // Timer between shootings.

    private Timer       shootingTimer,

    // Timer that times the slowing of the player.

                        slowTimer,

                        respawnTimer;

    // Object the player has equipped.

    private GameObject  pickupObject;

    // Boolean that indicates if the player has the remote.

    public bool         hasRemote,

    // Boolean that indicates if the player can move.

                        isDead;

    // Layer on which GameObjects can be picked up.

    public int          pickupLayer,

    // Score of the player.

                        score,

    // Health of the player.

                        health;

    // Max movement speed of the player.

    public float        maxMovementSpeed,

    // Delay between shootings.

                        shootDelay,

    // Force of the throwing of an object.

                        throwForce;

    // Inputs.

    public string       shootButton,

                        throwButton,

                        rotationAxisX,

                        rotationAxisY,

                        movementAxisY;

    // Spawnpoint of the bullets.

    public Transform    bulletSpawnPoint,

    // Position of an equipped object.

                        pickupPoint;

    // Prefab of the default bullet.

    public GameObject   bulletPrefab;

    // Start is called before the first frame update.

    void Start()
    {
        shootingTimer = new Timer(false, shootDelay);
        slowTimer = new Timer(false, 3f, 3f);
        respawnTimer = new Timer(false, 0f, 1f);

        minMovementSpeed = maxMovementSpeed / 2;
        currentSpeed = maxMovementSpeed;

        rotation = new Vector3(0f, 0f, 1f);
    }

    // Update is called once per frame.

    void Update()
    {
        if (!isDead)
            Move();

        Shoot();

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

        if(isDead)
        {
            CheckRespawn();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (pickupObject == null && collision.gameObject.layer == pickupLayer && collision.gameObject.transform.position.y < 1f)
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
                flagzone.ResetTimer();
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

    public void Die(Transform spawnPoint)
    {
        isDead = true;
        respawnTimer.Reset();
        transform.position = spawnPoint.position;
    }

    public void CheckRespawn()
    {
        if(respawnTimer.UpdateAndCheck())
        {
            SpawnPlayer();
           
        }
        Debug.Log(respawnTimer.GetElapsedTime());
    }

    public void SpawnPlayer()
    {
        health = 3;
        isDead = false;
    }

}
