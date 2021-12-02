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
    public float attackrange = 0.5f;
    public LayerMask enemyLayers;
    public float attackRate = 2;
    float nextAttackTime = 0f;
    int healthPoints;
    int maxHealth=100; //provisorischer wert
    //Attribute aus Spielmechaniken.pdf
    public int atk=40;  //physischer Schaden
    public int matk=0;  //magischer Schaden
    public int def=0; //Verteidigung
    public int mdef=0; //magische Verteidigung 
    public int str=0; //staerke
    public int dex=0; //Geschicklichkeit
    public int inte =0; // intelligenz
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
    public Vector2 position;

    void Start()
    {        	
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
        ui_Inventory.setInventory(inventory);    

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
        //Movement        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        //Kampf shit
        if(Time.time >= nextAttackTime)
        {
        //Attack
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
            nextAttackTime= Time.time + 1f / attackRate;
        }
        }
        //Debug Taste, alles möglich zum testen kann hier rein
        if(Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log(string.Format("Levelstand" + level));
            Debug.Log(string.Format("int" + this.inte));
            Debug.Log(string.Format("str" + str));
            Debug.Log(string.Format("dex" + dex));
            
            
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
    }

    void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        position=rb.position;
    }

    void Attack()
    {
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

    public void addExp(int xp)
    {
        exp+=xp;
        checkLevelup();
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
    
    void OnDrawGizmosSelected() //Schöner kreis im editor um den player herum, hat auf spiel keinen einfluss
    {
        if(attackpoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackpoint.position, attackrange);
    }   

    private void UseItem(Item item)
    {
        switch(item.itemtype)
        {          
            case Item.Itemtype.Sword: //eigentlich weglassen aber erstmal für demo drin, hat eigentlich keinen effekt, deswegen aus switch auslassen
            //effekt hier einfügen
            inventory.RemoveItem(new Item{itemtype = Item.Itemtype.Sword, amount=1});
            break;
            case Item.Itemtype.HealPotion: //effekt hier einfügen                    
                Debug.Log("Leben erhalten");
                healthPoints+=10;
                if(healthPoints>maxHealth) 
                {
                    healthPoints=maxHealth;
                }
             inventory.RemoveItem(new Item{itemtype = Item.Itemtype.HealPotion, amount=1});
             break;

        }
    }
}
