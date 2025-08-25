using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    [SerializeField] float reloadDelay = 2f;
    [SerializeField] ParticleSystem crashEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");

        if (other.gameObject.layer == layerIndex)
        {
            // SceneManager.LoadScene(0);
            crashEffect.Play();
            Invoke("ReloadScene", reloadDelay);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
