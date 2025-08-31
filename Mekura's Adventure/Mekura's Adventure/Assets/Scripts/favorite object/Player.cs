using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using animator;
using Unity.PlasticSCM.Editor.WebApi;

public class Player : LivingEntity
{
    public string PlayerName;
    public float speed;
    public float radiuceattackZona=0.9f;
    [SerializeField] private KeyCode Forward_button;
    [SerializeField] private KeyCode Left_button;
    [SerializeField] private KeyCode Back_button;
    [SerializeField] private KeyCode Right_button;
    [SerializeField] private KeyCode Bt_Action;
    private Rigidbody2D rb;
    private CircleCollider2D collider_attack_zona;
    public Transform attackzona;
    private int moveX;
    private int moveY;
    private anim anim;
    private SpriteRenderer spriteRenderer;
    private string LateAnim;
    private string CurrentAnim;
    private string[,] AnimationWalk;

    
    void Start()
    {
        Forward_button = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward_button"));
        Left_button = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left_button"));
        Back_button = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Back_button"));
        Right_button = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right_button"));
        Bt_Action = (KeyCode)Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Bt_Action"));
        AnimationWalk = new string[3,3] {
            {"WalkTopLeft",  "WalkTop", "WalkTopRight"}, 
            {"WalkLeft",  "Idle", "WalkRight"}, 
            {"WalkBackLeft",  "WalkBack", "WalkBackRight"}
        };
        collider_attack_zona = attackzona.GetComponent<CircleCollider2D>();
        collider_attack_zona.radius = radiuceattackZona;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<anim>();
        spriteRenderer = GetComponent<SpriteRenderer>();

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
                1, // ������ �����
                LayerMask.GetMask("Water") // ���� ������
            );
            foreach(Collider2D i in hitEnemies) 
            { 
                Destroy(i.gameObject);
            }
        
        }

        anim.SwitchAnimation(AnimationWalk[moveY+1, moveX+1]);
        attackzona.localPosition = new Vector3(moveX*radiuceattackZona, moveY* radiuceattackZona);
        

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
        Gizmos.DrawSphere(attackzona.position, radiuceattackZona);
    }

}
