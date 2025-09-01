using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField] PowerUpSO powerUp;

    PlayerController playerController;
    SpriteRenderer spriteRenderer;
    float timeLeft;

    void Start()
    {
        playerController = FindFirstObjectByType<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        timeLeft = powerUp.GetTime();
    }

    void Update()
    {
        CountdownTimer();
    }

    void CountdownTimer()
    {
        if (spriteRenderer.enabled == false)
        {
            
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                if (timeLeft < 0)
                {
                    playerController.DeactivatePowerup(powerUp); 
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Player");

        if (collision.gameObject.layer == layerIndex && spriteRenderer.enabled == true)
        {
            spriteRenderer.enabled = false;
            playerController.ActivatePowerup(powerUp);
        }
    }
}
