using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad; //what scene will load
    public Vector2 playerPos; //where the player will be
    public VectorValue storedPos; //the stored position of the player
    public Vector2 camNewMin;   //camera min
    public Vector2 camNewMax;   //camera max
    public VectorValue camMin;  //vector canera min
    public VectorValue camMax;  //vectoor camera max
    public GameObject fadeInPanel;  //the panel that emulates fading
    public GameObject fadeOutPanel; //the panel that emulates fading
    public float fadeWait;  //how long the scene waits until it changes

    private void Awake()
    {
        //plays the panel animation and destroys it when its done
        if(fadeInPanel != null)
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //if the player enters the transition area, sets the
        //player position and starts the fade coroutine
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            storedPos.initialVal = playerPos; //makes the player position the stored value

            StartCoroutine(FadeCo());
        }
    }

    public IEnumerator FadeCo()
    {
        //plays the fade animation and waits
        //until its done to change teh scene
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
        //changes the camera bounds
        camMax.initialVal = camNewMax;
        camMin.initialVal = camNewMin;
    }
}
