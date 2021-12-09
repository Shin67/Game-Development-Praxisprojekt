using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static SkillTree;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;

    public Animator animator;

    Vector2 movement;

    //Kampf shit
    public Transform attackpoint;
    public Transform attackpointLeft;
    public Transform attackpointRight;
    public Transform attackpointUp;
    public Transform attackpointDown;
    public float attackrange = 0.5f;
    public LayerMask enemyLayers;
    public float attackRate = 2;
    float nextAttackTime = 0f;
    public int healthPoints=100;
    public int maxHealth=100; //provisorischer wert

    public Weapon EquipedWeapon;
    public Shield EquipedShield;
    public Armor EquipedArmor;
    public bool meleeAttack=false;
    //Fernkampfshit
    public GameObject ArrowPrefabLeft;   
    public GameObject ArrowPrefabRight;    
    public GameObject ArrowPrefabUp;    
    public GameObject ArrowPrefabDown; 
    //Attribute aus Spielmechaniken.pdf
    public int atk=40;  //physischer Schaden
    public int matk=0;  //magischer Schaden
    public int def=0; //Verteidigung
    public int mdef=0; //magische Verteidigung 
    public int str=0; //staerke
    public int dex=0; //Geschicklichkeit
    public int inte =0; // intelligenz
    public int MP =0;
    public int MPMax=100;
    //Skills aus Skilltree
    public bool elementarPfeil = false;
    public bool elementarRegen = false;
    public bool scharfschuss = false;
    public bool elementarhieb = false;
    public bool elementarwirbel = false;
    public bool rage = false;
    public bool elementarball = false;
    public bool elementarflaeche = false;
    public bool sturmkette = false;
    public bool[] skillarray= new bool[9];      // haben ist besser als brauchen ¯\_(ツ)_/¯  
    //canvasSkilltree für skilltree
    public GameObject canvasSkilltree;
    public AudioSource audioSource;
    public AudioClip swordSound;
    public AudioClip walkingSound;
    bool canvasSkilltreeisActive;
    //Levelsystem
    public int exp=0;
    int level=0;
    //Vorläufige leveleinteilung, enthalten sind die mengen an nötigen xp
    int[] levelStufen = new int[] { 0,300,700,1200,1800,2400,   
                                    3100,3900,4700,5600,6600,
                                    7700,8900,10200,11600,13100,
                                    14700,16400,1900,2200,23000,
                                    25000,30000,35000,400000 }; 

    //InventoryShit
    private Inventory inventory;
    [SerializeField] private UI_Inventory ui_Inventory;
    //Canvas für Inventar
    public GameObject canvasInventory;
    bool canvasInventoryisActive;
<<<<<<< HEAD
    public Vector2 position;
    //ItemBools
    public bool HealPotionNormalEquiped=false;
    public bool HealPotionGroßEquiped=false;
    public bool  ManaTrankEquiped=false;
    public bool ManaTrankGroßEquiped=false;
    public bool MettBrotEquiped=false;
    public bool BlätterwasserEquiped=false;
    public bool LederRüstungEquiped=false;
    public bool KettenRüstungEquiped=false;
    public bool PlattenstahlRüstungEquiped=false;
    public bool HolzSchildEquiped=false;
    public bool EisenSchildEquiped=false;
    public bool StahlSchildEquiped=false;
    public bool BeginnerBogenEquiped=false;
    public bool JägerBogenEquiped=false;
    public bool AkolythenstabEquiped=false;
    public bool ElementarstabEquiped=false;
    public bool MeisterStabEquiped=false;
    public bool SchwertEquiped=false;
    public bool KampfAxtEquiped=false;
    public bool StreitKolbenEquiped=false;

    AudioSource attackSound;
=======
    public Vector2 position; 

   //UI Status Bar
   public StatusBar healthBar;
   public StatusBar manaBar;
    //potentielles Equipment
    public Weapon BeginnerBogen = new Weapon(10,Weapon.WeaponType.Bow);
    public Weapon JägerBogen = new Weapon(20,Weapon.WeaponType.Bow);
    public Weapon Akolythenstab = new Weapon(40,Weapon.WeaponType.Staff);
    public Weapon Elementarstab = new Weapon(60,Weapon.WeaponType.Staff);
    public Weapon MeisterStab = new Weapon(80,Weapon.WeaponType.Staff);
    public Weapon Schwert = new Weapon(40,Weapon.WeaponType.Melee);
    public Weapon KampfAxt = new Weapon(50,Weapon.WeaponType.Melee);
    public Weapon StreitKolben = new Weapon(60,Weapon.WeaponType.Melee); 
>>>>>>> main
    
    public Shield HolzSchild = new Shield(10,10);    
    public Shield EisenSchild = new Shield(20,15);   
    public Shield StahlSchild = new Shield(30,20); 

    public Armor LederRüstung = new Armor(10);     
    public Armor KettenRüstung = new Armor(20); 
    public Armor PlattenstahlRüstung = new Armor(30);   

    public enum Direction
   {
       Left,
       Right,
       Up,
       Down

   }                                                                           

    void Start()
    {        	
        
        AudioSource[] audioSources = GetComponents<AudioSource>();
        audioSources[0].clip = walkingSound;
        audioSources[1].clip = swordSound;
        audioSource = audioSources[0];
        attackSound = audioSources[1];
        DontDestroyOnLoad(gameObject);  //damit player in neuer szene erhalten bleibt
        //Bug rudimentär gefixed, keine ahnung was das problem eigentlich ist ¯\_(ツ)_/¯
        canvasSkilltree.SetActive(false);
        canvasSkilltreeisActive=false;
        canvasInventory.SetActive(false);
        canvasInventoryisActive=false;
        //Array mit allen durch den Baum erhaltenden Skills(Ka ob jemals gebraucht, war für etwas anderes geplant)
        skillarray = new bool[] {elementarPfeil,elementarRegen,
                                            scharfschuss,elementarhieb,
                                            elementarwirbel,rage,elementarball,
                                            elementarflaeche,sturmkette};

        //InventoryShit
        inventory=new Inventory(UseItem);
<<<<<<< HEAD
        ui_Inventory.setInventory(inventory);
=======
        ui_Inventory.setInventory(inventory);  
        //Standardwaffen erstellen
        
      
>>>>>>> main

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemworld = collider.GetComponent<ItemWorld>();
        if(itemworld != null)
        {
            if(inventory.inhalt<inventory.capacity)
            {
                //Debug.Log("Item berührt");
                inventory.addItem(itemworld.getItem());
                //Debug.Log(inventory.getItemList().Count);
                 itemworld.DestroySelf();
            }
           
        }
    }
   

    void Update()
    {   
        //Movement sound
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            audioSource.volume = 0.04f;
            if(!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        } else
        {
            audioSource.Stop();
        }

        //Movement        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        //Kampf shit
        if(EquipedWeapon!=null){
            if(EquipedWeapon.weaponType == Weapon.WeaponType.Melee)
        {
<<<<<<< HEAD
                attackSound.volume = 0.4f;
            attackSound.Play();
            Attack();
            nextAttackTime= Time.time + 1f / attackRate;
=======
            meleeAttack=true;
>>>>>>> main
        }
        else{
            meleeAttack=false;
        }
        }
        
        if(Time.time >= nextAttackTime)
        {
            if(EquipedWeapon!=null)
            {

                if(Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if(meleeAttack){
                        Attack(Direction.Up);
                    }else{
                        RangeAttack(Direction.Up);
                        Debug.Log("UP range attack");
                    }
                    nextAttackTime= Time.time + 1f / attackRate;
                    
                }
                if(Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if(meleeAttack){
                        Attack(Direction.Down);
                    }else{
                        RangeAttack(Direction.Down);
                    }
                    nextAttackTime= Time.time + 1f / attackRate;
                }
                if(Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if(meleeAttack){
                        Attack(Direction.Left);
                    }else{
                        RangeAttack(Direction.Left);
                    }
                    nextAttackTime= Time.time + 1f / attackRate;
                }
                if(Input.GetKeyDown(KeyCode.RightArrow))
                {
                    if(meleeAttack){
                        Attack(Direction.Right);
                    }else{
                        RangeAttack(Direction.Right);
                    }
                    nextAttackTime= Time.time + 1f / attackRate;
                }
                    }      

        }
        //Debug Taste, alles möglich zum testen kann hier rein
        if(Input.GetKeyDown(KeyCode.X))
        {   
            
            
        }
        //Skilltree aufrufen
        if(Input.GetKeyDown(KeyCode.T))
        {
            if(canvasSkilltreeisActive){
                canvasSkilltree.SetActive(false);
                canvasSkilltreeisActive=false;

            }else{
                canvasSkilltree.SetActive(true);
                canvasSkilltreeisActive=true;
            }
        }   
        //Inventar aufrufen
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(canvasInventoryisActive){
                canvasInventory.SetActive(false);
                canvasInventoryisActive=false;
                

            }else{
                canvasInventory.SetActive(true);
                canvasInventoryisActive=true;
            }
        }

        //Update UI Statusbar
        healthBar.setMaxValue(maxHealth);
        healthBar.setValue(healthPoints);

        manaBar.setMaxValue(MPMax);
        manaBar.setValue(MP);     
    }

    void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        position=rb.position;
    }

    void Attack(Direction direction)
    {
<<<<<<< HEAD
       
=======
        if(direction == Direction.Left)
        {
            Debug.Log("Left");
            //animator.SetTrigger("Attack_Left");
            attackpoint=attackpointLeft;
        }
        if(direction == Direction.Right)
        {
            Debug.Log("right");
            //animator.SetTrigger("Attack_Right");
            attackpoint=attackpointRight;
        }
        if(direction == Direction.Up)
        {
            Debug.Log("up");
            //animator.SetTrigger("Attack_Up");
            attackpoint=attackpointUp;
        }
        if(direction == Direction.Down)
        {
            Debug.Log("down");
            //animator.SetTrigger("Attack_Down");
            attackpoint=attackpointDown;
        }
>>>>>>> main
        //Animation
        animator.SetTrigger("Attack");
        //Attack enemies
        Collider2D [] hitEnemies= Physics2D.OverlapCircleAll(attackpoint.position, attackrange, enemyLayers);
        //dmg to enemies
        foreach(Collider2D enemy in hitEnemies)
        {
             
                Debug.Log("Hit"+enemy.name);
                enemy.GetComponent<fightable>().TakeDMG(atk);             
            
        }
    } 


        
    void RangeAttack(Direction direction)
    {
        if(direction == Direction.Left)
        {               
            Instantiate(ArrowPrefabLeft, attackpointLeft.position, attackpoint.rotation);
        }
        if(direction == Direction.Right)
        {
            Instantiate(ArrowPrefabRight, attackpointRight.position, attackpoint.rotation);
        }
        if(direction == Direction.Up)
        {
            Instantiate(ArrowPrefabUp, attackpointUp.position, attackpoint.rotation);
        }
        if(direction == Direction.Down)
        {
            Instantiate(ArrowPrefabDown, attackpointDown.position, attackpoint.rotation);
        }             
    }

    public void addExp(int xp)
    {
        exp+=xp;
        checkLevelup();
    }

    public void addHealth(int amount){
        if (healthPoints + amount > maxHealth){
            healthPoints = maxHealth;
        } else {
            healthPoints += amount;
        }
        
    }

    public void addMP(int amount){
        if (MP + amount > MPMax){
            MP = MPMax;
        } else {
            MP += amount;
        }
    }
    void checkLevelup()
    {
        int temp =0;
        foreach(int lv in levelStufen)  //Index(Level) wird gesucht, ab dem die exp kleiner sind als im Verzeichnis steht -> level wird zu den exp berechnet
        {
            if(lv < exp)
            {
                temp++;
            }
            else
            {
                break;
            }
        }
        Debug.Log(temp);
        if(temp>level) //ist das errechnete Level größer als das aktuelle liegt ein levelup vor
        {             
            //Debug.Log(temp-level);
            skillTree.SkillPoints+=(temp-level);
            skillTree.LevelupSkillpoints+=(temp-level);
            level=temp;
            //Werden mehrere Level auf einmal erreicht(was zu vermeiden ist) funzt das system trotzdem,
        }
    }

    private void UseItem(Item item)
    {
        switch(item.itemtype)
        {          
                            
           default:
           case Item.Itemtype.HealPotionNormal:   
                healthPoints+=10;
                    if(healthPoints>maxHealth) 
                    {
                        healthPoints=maxHealth;
                    }
                inventory.RemoveItem(new Item{itemtype = Item.Itemtype.HealPotionNormal, amount=1});     
           break;
           case Item.Itemtype.HealPotionGroß:
                healthPoints+=50;
                if(healthPoints>maxHealth) 
                {
                    healthPoints=maxHealth;
                }
                inventory.RemoveItem(new Item{itemtype = Item.Itemtype.HealPotionGroß, amount=1});   
           break;
           case Item.Itemtype.ManaTrank:   
                MP+=10;
                if(MP>MPMax) 
                {
                    MP=MPMax;
                }
                inventory.RemoveItem(new Item{itemtype = Item.Itemtype.ManaTrank, amount=1});   
           break;
           case Item.Itemtype.ManaTrankGroß:
                MP+=50;
                if(MP>MPMax) 
                {
                    MP=MPMax;
                }
                inventory.RemoveItem(new Item{itemtype = Item.Itemtype.ManaTrankGroß, amount=1});           
           break;
           case Item.Itemtype.MettBrot:   
                healthPoints+=20;
                if(healthPoints>maxHealth) 
                {
                    healthPoints=maxHealth;
                }
                inventory.RemoveItem(new Item{itemtype = Item.Itemtype.MettBrot, amount=1});     
           break;
           case Item.Itemtype.Blätterwasser:   
           //TODO
           break;
           case Item.Itemtype.LederRüstung:  
           EquipedArmor = LederRüstung;
           item.isequipped=true;    
            //TODO
           break;
           case Item.Itemtype.KettenRüstung:  
           EquipedArmor = KettenRüstung;
           item.isequipped=true;       
            //TODO
           break;
           case Item.Itemtype.PlattenstahlRüstung:
           EquipedArmor = PlattenstahlRüstung;
           item.isequipped=true;
            //TODO
           break;
           case Item.Itemtype.HolzSchild:   
           item.isequipped=true;
           EquipedShield = HolzSchild;
            //TODO
           break;
           case Item.Itemtype.EisenSchild: 
           item.isequipped=true;
           EquipedShield = EisenSchild;
            //TODO   
           break;
           case Item.Itemtype.StahlSchild:  
           item.isequipped=true; 
           EquipedShield = StahlSchild;
            //TODO          
           break;
           case Item.Itemtype.BeginnerBogen:   
            //TODO
            EquipedWeapon = BeginnerBogen;
            item.isequipped=true;
           break;
           case Item.Itemtype.JägerBogen:  
            //TODO  
            EquipedWeapon = JägerBogen;
            item.isequipped=true;
           break;
           case Item.Itemtype.Akolythenstab:   
           //TODO
           EquipedWeapon = Akolythenstab;
           item.isequipped=true;
           break;
           case Item.Itemtype.Elementarstab:  
           //TODO 
           EquipedWeapon =Elementarstab;
           item.isequipped=true;
           break;
           case Item.Itemtype.MeisterStab:    
           //TODO   
           EquipedWeapon = MeisterStab;
           item.isequipped=true;
           break;
           case Item.Itemtype.Schwert:   
           //TODO 
           EquipedWeapon = Schwert;
           item.isequipped=true;
           break;
           case Item.Itemtype.KampfAxt:   
           //TODO
           EquipedWeapon = KampfAxt;
           item.isequipped=true;
           break;
           case Item.Itemtype.StreitKolben:  
           //TODO 
           EquipedWeapon = StreitKolben;
           item.isequipped=true;
           break;

             

        }
    }
    
    void OnDrawGizmosSelected() //Schöner kreis im editor um den player herum, hat auf spiel keinen einfluss
    {
        if(attackpoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackpoint.position, attackrange);
    }   


    
}
