using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsterScript : EnemyScript
{
    #region Fields

    #endregion

    #region References
    //Reference to player script, could be put in abstract class
    //Reference to camera, can be in abstract class
    #endregion

    #region Properties


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //Set variables to starting values

    }

    // Update is called once per frame
    void Update()
    {
        //Call attack and movement if on screen
        if(OnScreen)
        {
            Attack();
            Movement();
        }
    }

    private void FixedUpdate()
    {
        
    }


    public override void Attack()
    {
        //Check attack cooldown
        if(attackCooldown <= 0)
        {
            if(true) //Test if player is near/lined up
            {
                //Attack, spawn a bullet

                //Reset attack cooldown
                attackCooldown = MAX_CD;
            }
        }
        else //Decreases cooldown
        {
            attackCooldown -= 1f * Time.deltaTime;
        }
    }

    public override void Movement()
    {
        
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        //Play hit animation and effect


        //Test if the character has taken enough damage to die
        if(health <= 0)
        {
            //Play death animation/effect

            //After effect finishes, destroy
            Destroy(this.gameObject);
        }
    }
}
