using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float baseSpeed = 15f;
    [SerializeField] float boostSpeed = 20f;
    [SerializeField] ParticleSystem powerupParticles;

    Vector2 moveVector;
    bool canControlPlayer = true;
    float previousRotation;
    float totalRotation;
    int activePowerupCount = 0;


    InputAction moveAction;
    Rigidbody2D myRigidbody2D;
    SurfaceEffector2D surfaceEffector2D;
    ScoreManager scoreManager;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        myRigidbody2D = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindFirstObjectByType<SurfaceEffector2D>();
        scoreManager = FindFirstObjectByType<ScoreManager>();
    }

    void CalculateFlips()
    {
        float currentRotation = transform.rotation.eulerAngles.z;

        totalRotation += Mathf.Abs(currentRotation - previousRotation);

        if (totalRotation > 360 || totalRotation < -360)
        {
            // flipCount++;
            totalRotation = 0;
            // Debug.Log("Flips: " + flipCount);
            scoreManager.AddScore(100);
        }

        previousRotation = currentRotation;
    }

    void Update()
    {
        if (canControlPlayer)
        {
            RotationPlayer();
            BoostPlayer();
            CalculateFlips();
        }

    }

    void RotationPlayer()
    {

        moveVector = moveAction.ReadValue<Vector2>();
        if (moveVector.x < 0)
        {
            myRigidbody2D.AddTorque(torqueAmount);
        }
        else if (moveVector.x > 0)
        {
            myRigidbody2D.AddTorque(-torqueAmount);
        }
    }

    void BoostPlayer()
    {

        if (moveVector.y > 0)
        {
            surfaceEffector2D.speed = boostSpeed;
        }
        else
        {
            surfaceEffector2D.speed = baseSpeed;
        }
    }

    public void DisableControl()
    {
        canControlPlayer = false;
    }

    public void ActivatePowerup(PowerUpSO powerUp)
    {
        powerupParticles.Play();
        activePowerupCount++;
        if (powerUp.GetPowerUpType() == "speed")
        {
            baseSpeed += powerUp.GetValueChange();
            boostSpeed += powerUp.GetValueChange();
        }
        else if (powerUp.GetPowerUpType() == "torque")
        {
            torqueAmount += powerUp.GetValueChange();
        }
    }

    public void DeactivatePowerup(PowerUpSO powerUp)
    {
        activePowerupCount--;
        if (activePowerupCount <= 0)
        {
            powerupParticles.Stop();
            activePowerupCount = 0;
        }
        if (powerUp.GetPowerUpType() == "speed")
        {
            baseSpeed -= powerUp.GetValueChange();
            boostSpeed -= powerUp.GetValueChange();
        }
        else if (powerUp.GetPowerUpType() == "torque")
        {
            torqueAmount -= powerUp.GetValueChange();
        }
    }
}