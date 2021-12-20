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

    public LayerMask enemyLayers;

    public int healthPoints=100;
    public int maxHealth=100;

    public Weapon EquipedWeapon;
    public Armor EquipedArmor;

    //Attributes
    public int atk=40;
    public int magicAtk=0;
    public int def=0; //Verteidigung
    public int mdef=0; //magische Verteidigung 
    public int str=0; //staerke
    public int dex=0; //Geschicklichkeit
    public int inte =0; // intelligenz
    public int MP =0;
    public int MPMax=100;

    //canvasSkilltree for skilltree
    public GameObject canvasSkilltree;
    
    bool canvasSkilltreeisActive;
    //Levelsystem
    public int exp=0;
    int level=0;
    
    int[] expToLevel = new int[] { 0,300,700,1200,1800,2400,   
                                    3100,3900,4700,5600,6600,
                                    7700,8900,10200,11600,13100,
                                    14700,16400,1900,2200,23000,
                                    25000,30000,35000,400000 }; 

    //Inventory
    private Inventory inventory;
    [SerializeField] private UI_Inventory ui_Inventory;
    //Inventory Canvas
    public GameObject canvasInventory;
    bool canvasInventoryisActive;
    public Vector2 position;
    public float knockbackSpeed = 400f;
    

    AudioSource attackSound;
    public AudioSource audioSource;
    public AudioClip swordSound;
    public AudioClip walkingSound;

    //UI Status Bar
    public StatusBar healthBar;
    public StatusBar manaBar;
    public Text UIcurrentLevel;

    void Start()
    {
        AudioSource[] audioSources = GetComponents<AudioSource>();
        audioSources[0].clip = walkingSound;
        audioSources[1].clip = swordSound;
        audioSource = audioSources[0];
        attackSound = audioSources[1];
        DontDestroyOnLoad(gameObject);  // TODO: bug fixed, but look up
        canvasSkilltree.SetActive(false);
        canvasSkilltreeisActive=false;

        //Inventory
        inventory=new Inventory();
        ui_Inventory.setInventory(inventory);
        ui_Inventory.setInventory(inventory);  
        
      
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        ItemWorld itemworld = collider.GetComponent<ItemWorld>();
        if(itemworld != null)
        {
            if(inventory.inhalt<inventory.capacity)
            {
                inventory.addItem(itemworld.getItem());
                 itemworld.DestroySelf();
            }
           
        }
    }

    void FixedUpdate()
    {
        //Movement sound
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            audioSource.volume = 0.04f;
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }

        //Movement        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        //Skilltree
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (canvasSkilltreeisActive)
            {
                canvasSkilltree.SetActive(false);
                canvasSkilltreeisActive = false;

            }
            else
            {
                canvasSkilltree.SetActive(true);
                canvasSkilltreeisActive = true;
            }
        }
        //Inventory 
        if (Input.GetKeyDown(KeyCode.I))
        {
            ui_Inventory.switchVisiblity();

        }

        //Update UI Statusbar
        healthBar.setMaxValue(maxHealth);
        healthBar.setValue(healthPoints);

        manaBar.setMaxValue(MPMax);
        manaBar.setValue(MP);

        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        position=rb.position;
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
        foreach(int lv in expToLevel)
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
        if(temp>level)
        {             
            skillTree.SkillPoints+=(temp-level);
            skillTree.LevelupSkillpoints+=(temp-level);
            level=temp;
            UIcurrentLevel.text = level.ToString();
        }
    }

    public void pushBack(Vector2 enemyPosition)
    {
        var direction = (position - enemyPosition).normalized;
        rb.AddForce(direction * knockbackSpeed);
    }
}
