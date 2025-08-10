using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : LivingEntity
{
    public string PlayerName;
    public float speed;
    [SerializeField] private KeyCode Forward_button;
    [SerializeField] private KeyCode Left_button;
    [SerializeField] private KeyCode Back_button;
    [SerializeField] private KeyCode Right_button;
    [SerializeField] private KeyCode Bt_Action;
    private Rigidbody2D rb;
    public Transform attackzona;
    private float moveX;
    private float moveY;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.zero;
        moveY = 0;
        moveX = 0;
        if (Input.GetKey(Forward_button)) moveY = 1;
        if (Input.GetKey(Back_button)) moveY = -1;
        if (Input.GetKey(Left_button)) moveX = -1;
        if (Input.GetKey(Right_button)) moveX = 1;
        rb.velocity = new Vector2(moveX * speed, moveY*speed);
        if (Input.GetKey(Bt_Action))
        {
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(
                transform.position,
                1, // Радиус атаки
                LayerMask.GetMask("Water") // Слой врагов
            );
            foreach(Collider2D i in hitEnemies) 
            { 
                Destroy(i.gameObject);
            }
        
        }
    }
    protected override void Attack(LivingEntity target)
    {
        target.GetComponent<LivingEntity>().TakeDamage(attackDamageMin);
    }
    public override void Interact(Entity other)
    {
        other.DestroySelf();
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(attackzona.position, 1);
    }

}
