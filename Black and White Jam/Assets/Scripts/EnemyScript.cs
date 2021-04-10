using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyScript : MonoBehaviour
{
    #region Fields
    protected int health;
    [SerializeField] protected int MAX_HEALTH;

    protected float attackCooldown;
    [SerializeField] protected float MAX_CD;

    protected bool OnScreen;

    #endregion

    #region Properties
    public int Health { get { return health; } set { health = value; } }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    /// <summary>
    /// Abstract method used to attack the player.
    /// </summary>
    public virtual void Attack()
    {
        Debug.Log("Attack method in " + this.gameObject + " is not overwritten.");
    }

    /// <summary>
    /// Abstract method that dictates this character's movement pattern.
    /// </summary>
    public virtual void Movement()
    {
        Debug.Log("Movement method in " + this.gameObject + " is not overwritten.");
    }

    /// <summary>
    /// Method called when this character takes damage.
    /// </summary>
    public virtual void TakeDamage(int damage)
    {
        health -= damage;
    }

}
