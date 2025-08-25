using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        int layerIndex = LayerMask.NameToLayer("Player");
       
       if (other.gameObject.layer == layerIndex)
        {
            SceneManager.LoadScene(0);
        }
    }

}
