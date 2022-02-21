using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; //where the camera should be
    public float smoothing; //how smooth the camera moves
    public Vector2 maxPosition; //the max position the camera should be at
    public Vector2 minPosition; //the min position the camera should be at
    public Animator anim;
    public VectorValue camMin;
    public VectorValue camMax;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        anim = GetComponent<Animator>();

        maxPosition = camMax.initialVal;
        minPosition = camMin.initialVal;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //if the camera position and the player position aren't the same
        if(transform.position != target.position)
        {
            //changes the target position when the player moves
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            //makes sure the camera doesn't move to far up, down, left, or right
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            //sets the smoothness of the camera
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }

    public void BeginKick()
    {
        anim.SetBool("kickActive", true);
        StartCoroutine(KickCo());
    }

    public IEnumerator KickCo()
    {
        yield return null;
        anim.SetBool("kickActive", false);
    }
}
