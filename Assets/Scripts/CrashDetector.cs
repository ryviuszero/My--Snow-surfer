using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        int layerIndex = LayerMask.NameToLayer("Floor");
       
       if (other.gameObject.layer == layerIndex)
        {
            SceneManager.LoadScene(0);
        }
    }
}
