using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
   // Damage struct

   public int[] damagePoint = {1, 3, 6};
   public float[] pushForce = {2.0f, 3.0f, 4.0f};

   //Upgrade
   public int weaponLevel = 0;
   public SpriteRenderer spriteRenderer;

   //Swing
   private float cooldown = 0.5f;
   private float lastSwing;
   private Animator anim;

   private void Awake()
   {
    spriteRenderer = GetComponent<SpriteRenderer>();

   }

   protected override void Start()
   {
        base.Start();
        
        anim = GetComponent<Animator>();
   }

   protected override void Update()
   {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
   }

   protected override void OnCollide(Collider2D coll)
   {
        if(coll.tag == "Fighter")
        {
            if(coll.name == "Player")
                return;

            //Create new damage object and send it to fighter that has been hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };

            coll.SendMessage("ReceiveDamage", dmg);
            
        }
        
   }

   private void Swing()
   {
        anim.SetTrigger("Swing");
   }

   public void UpgradeWeapon()
   {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

   
   }

   public void SetWeaponLevel(int level)
   {
    weaponLevel = level;
    spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
   }
}
