using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts;  //the hearts ui
    public Sprite fullHeart;    //the sprite of the full heart
    public Sprite halfHeart;    //the sprite of the half heart
    public Sprite emptyHeart;   //the sprite of the empty heart
    public FloatValue heartContainers;  //the max health
    public FloatValue currHealth;   //the current health of the player

    // Start is called before the first frame update
    void Start()
    {
        InitHearts();   //sets the hearts ui
    }

    private void Update()
    {
        UpdateHearts(); //updates the hearts ui
    }

    public void InitHearts()
    {
        //sets up that hearts ui
        for(int i=0; i < heartContainers.RuntimeValue; i++)
        {
            if (i < hearts.Length)
            {
                hearts[i].gameObject.SetActive(true);
                hearts[i].sprite = fullHeart;
            }

        }
    }

    public void UpdateHearts()
    {
        //updates the hearts ui when the player loses or gains health
        InitHearts();
        float tempHealth = currHealth.RuntimeValue / 2;

        for (int i = 0; i < heartContainers.RuntimeValue; i++) {
            if (i <= tempHealth-1)
            {
                hearts[i].sprite = fullHeart;
            }
            else if (i >= tempHealth)
            {
                hearts[i].sprite = emptyHeart;
            }
            else
            {
                hearts[i].sprite = halfHeart;
            }
        }
    }
}
