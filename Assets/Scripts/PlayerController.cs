using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float baseSpeed = 15f;
    [SerializeField] float boostSpeed = 20f;

    Vector2 moveVector;
    bool canControlPlayer = true;
    float previousRotation;
    float totalRotation;
    int flipCount;


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

        if (totalRotation >= 340 || totalRotation <= -340)
        {
            flipCount++;
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
}
