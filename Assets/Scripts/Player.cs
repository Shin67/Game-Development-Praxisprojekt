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
    


    //Canvas für skilltree
    public GameObject canvas;
    bool canvasisActive;
    //Levelsystem
    public int exp=0;
    int level=0;
    //Vorläufige leveleinteilung
    int[] levelStufen = new int[] { 0,300,700,1200,1800,2400,   // enthalten sind die mengen an nötigen xp
                                    3100,3900,4700,5600,6600,
                                    7700,8900,10200,11600,13100,
                                    14700,16400,1900,2200,23000,
                                    25000,30000,35000,400000 }; 

    void Start(){
        canvas.SetActive(false);
        canvasisActive=false;

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
            if(canvasisActive){
                canvas.SetActive(false);
                canvasisActive=false;

            }else{
                canvas.SetActive(true);
                canvasisActive=true;


            }
        }

        
    }

    void FixedUpdate()
    {
        //Movement
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void Attack()
    {
        //Animation
        animator.SetTrigger("Attack");
        //Attack enemies
        Collider2D [] hitEnemies= Physics2D.OverlapCircleAll(attackpoint.position, attackrange, enemyLayers);
        //dmg to enemies
        foreach(Collider2D enemy in hitEnemies){
            Debug.Log("Hit"+enemy.name);
            enemy.GetComponent<fightable>().TakeDMG(atk);
        }
    } 

    public void addExp(int xp){
        exp+=xp;
        checkLevelup();
    }

    void checkLevelup(){
        int temp =0;
        foreach(int lv in levelStufen)  //Index(Level) wird gesucht, ab dem die exp kleiner sind als im Verzeichnis steht -> level wird zu den exp berechnet
        {
            if(lv < exp){
                temp++;
            }
            else{
                break;
            }
        }
        Debug.Log(temp);
        if(temp>level){             //ist das errechnete Level größer als das aktuelle liegt ein levelup vor
            Debug.Log(temp-level);
            skillTree.SkillPoints+=(temp-level);
            skillTree.LevelupSkillpoints+=(temp-level);

            level=temp;
        //Werden mehrere Level auf einmal erreicht(was zu vermeiden ist) funzt das system trotzdem,
        }
    }
    

    
    void OnDrawGizmosSelected()
    {
        if(attackpoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackpoint.position, attackrange);
    }   


}
