using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad; //what scene will load
    public Vector2 playerPos; //where the player will be
    public VectorValue storedPos; //the stored position of the player
    public Vector2 camNewMin;
    public Vector2 camNewMax;
    public VectorValue camMin;
    public VectorValue camMax;
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait;

    private void Awake()
    {
        if(fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            storedPos.initialVal = playerPos; //makes the player position the stored value

            StartCoroutine(FadeCo());
        }
    }

    public IEnumerator FadeCo()
    {
        if(fadeOutPanel != null)
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }

        //yield return new WaitForSeconds(fadeWait);
        ResetCameraBounds();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    public void ResetCameraBounds()
    {
        camMax.initialVal = camNewMax;
        camMin.initialVal = camNewMin;
    }
}
