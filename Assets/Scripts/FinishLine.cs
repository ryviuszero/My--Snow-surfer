using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float reloadDelay = 2f;
    [SerializeField] ParticleSystem finishEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        int layerIndex = LayerMask.NameToLayer("Player");

        if (other.gameObject.layer == layerIndex)
        {
            // SceneManager.LoadScene(0);
            finishEffect.Play();
            Invoke("ReloadScene", reloadDelay);
        }
    }

    void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }

}
