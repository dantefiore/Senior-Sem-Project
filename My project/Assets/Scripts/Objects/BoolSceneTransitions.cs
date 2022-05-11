using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolSceneTransitions : SceneTransition
{
    [SerializeField] private BoolVal storyPoint;

    // Start is called before the first frame update
    void Start()
    {
        //if the story point is not completed, the player cannot go to that scene,
        //if it is, then the scene is available to go to
        if (storyPoint.RuntimeValue)
            this.gameObject.SetActive(true);
        else if (!storyPoint.RuntimeValue)
            this.gameObject.SetActive(false);
    }
}
