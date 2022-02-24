using UnityEngine;

public class GenericHealth : MonoBehaviour
{
    //SerializeField means the variable can be manipulated
    //in the inspector but not to other classes
    public FloatValue maxHealth;
    public float currHealth;


    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth.RuntimeValue;
    }

    public virtual void Heal(float amount)
    {
        currHealth += amount;

        if (currHealth > maxHealth.RuntimeValue)
        {
            currHealth = maxHealth.RuntimeValue;
        }
    }

    public virtual void fullHeal()
    {
        currHealth = maxHealth.RuntimeValue;
    }

    public virtual void Damage(float amount)
    {
        currHealth -= amount;

        if (currHealth < 0)
        {
            currHealth = 0;
        }
    }

    public virtual void InstantDeath()
    {
        currHealth = 0;
    }
}
