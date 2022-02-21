using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    //where to set the camera and its bounds for the room transition
    public Vector2 cameraMin;
    public Vector2 cameraMax;

    //where to set the new player position for the room transition
    public Vector3 playerChange;

    //the CameraMovement file
    private CameraMovement cam;

    public bool needText; //does this transition need text
    public string locName; //name of place
    public GameObject text; //the location text box
    public Text locText; //the text of the location text box


    // Start is called before the first frame update
    void Start()
    {
        //finishing the connection
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //when the transition is in contact with the player
    private void OnTriggerEnter2D(Collider2D other)
    {
        //if its the player that made contact with the trigger
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            //new camera boundries
            cam.minPosition += cameraMin;
            cam.maxPosition += cameraMax;

            //setting the new player position
            other.transform.position += playerChange;

            if (needText)
            {
                StartCoroutine(placeNameCo());
            }
        }
    }

    private IEnumerator placeNameCo()
    {
        text.SetActive(true);
        locText.text = locName;

        yield return new WaitForSeconds(4f);

        text.SetActive(false);
    }
}
