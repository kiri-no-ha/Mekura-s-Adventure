using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : LivingEntity
{
    public string PlayerName;
    public float speed;
    [SerializeField] private KeyCode W;
    [SerializeField] private KeyCode A;
    [SerializeField] private KeyCode S;
    [SerializeField] private KeyCode D;
    [SerializeField] private KeyCode Bt_Action;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        rb.velocity = new Vector2 (moveX*speed, moveY*speed);
    }
    protected override void Attack(LivingEntity target)
    {
        target.GetComponent<LivingEntity>().TakeDamage(attackDamageMin);
    }
}
