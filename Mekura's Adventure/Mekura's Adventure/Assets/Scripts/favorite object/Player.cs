using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using animator;

public class Player : LivingEntity
{
    public string PlayerName;
    public float speed;
    [SerializeField] private KeyCode Up;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode down;
    [SerializeField] private KeyCode right;
    [SerializeField] private KeyCode Bt_Action;
    private Rigidbody2D rb;
    private anim anim;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<anim>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");
        rb.velocity = new Vector2 (moveX*speed, moveY*speed);
        if (Input.GetKeyDown(Up))
        {
            anim.SwitchAnimation("up");
        }
        if (Input.GetKeyDown(down))
        {
            anim.SwitchAnimation("down");
        }
        if (Input.GetKeyUp(down) && Input.GetKeyDown(Up)) { anim.Stop(); }

    }
    protected override void Attack(LivingEntity target)
    {
        target.GetComponent<LivingEntity>().TakeDamage(attackDamageMin);
    }
}
