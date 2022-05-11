using UnityEngine;

public class GenericHealth : MonoBehaviour
{
    public FloatValue maxHealth;    //the player's max health
    public float currHealth;    //the player's current health
    public FloatValue death_count;  //the death counter

    // Start is called before the first frame update
    void Start()
    {
        //sets the current health of the player
        currHealth = maxHealth.RuntimeValue;
    }

    public virtual void Update()
    {
        //keeps track of the object is at or lower than 0
        if(currHealth <= 0)
        {
            death_count.RuntimeValue += 1;
            this.gameObject.SetActive(false);
        }
    }

    public virtual void Heal(float amount)
    {
        //increases the health
        currHealth += amount;

        //if the health is higher than the max, it sets it back to max
        if (currHealth > maxHealth.RuntimeValue)
        {
            currHealth = maxHealth.RuntimeValue;
        }
    }

    public virtual void fullHeal()
    {
        //sets health to full
        currHealth = maxHealth.RuntimeValue;
    }

    public virtual void Damage(float amount)
    {
        //decreases the health
        currHealth -= amount;

        //if the health is lower than 0, it sets it back to 0
        if (currHealth < 0)
        {
            currHealth = 0;
        }
    }

    public virtual void InstantDeath()
    {
        // lowers the health to 0
        currHealth = 0;
    }
}
