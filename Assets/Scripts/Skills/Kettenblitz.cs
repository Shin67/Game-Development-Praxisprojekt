using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kettenblitz : MonoBehaviour
{

    private Player caster;
    private int damage;
    private int maxTargets = 5;
    private int range = 20;
    private GameObject[] targets;
    private bool[] targetsHit;
    private int numOfHits = 0;
    private int timeToLive = 500;
    private LineRenderer lr;
    private void Awake(){
        //Setup damit alles ordentlich funktioniert
        caster = Player.getInstance();                          //Get Player Instance and get Player-Intelligence
        damage = caster.inte;                                   //Player-Intelligence = Damage (TODO: Set reasonable damage)
        targets = GameObject.FindGameObjectsWithTag("Enemy");   //Gibt alle Enemies in current Szene
        targetsHit = new bool[targets.Length];                  //Create an array um zu merken welcher Gegner bereits gehittet wurde
        for (int i = 0; i < targetsHit.Length; i++){
            targetsHit[i] = false;                              //Am Anfang wurde noch niemand gehittet
            Debug.Log("Target: " + targets[i].name + " Distance: " + Vector3.Distance(transform.position,targets[i].transform.position));
        }

        //Setup Line-Renderer, damit man auch visuell was sieht
        lr = gameObject.AddComponent<LineRenderer>();
        lr.startColor = lr.endColor = Color.cyan;
        lr.alignment = LineAlignment.View;
        lr.startWidth = lr.endWidth = 0.3f;
        lr.positionCount = 1;
        lr.SetPosition(0, transform.position);  //Start at Cast Origin
        lr.sortingLayerName = "Actor";
        lr.material = new Material(Shader.Find("Sprites/Default"));
        
        //Soundeffekt abspielen
        AudioManager.getInstance().Play("Kettenblitz");

        //MainLoop starten
        StartCoroutine(mainLoop());
        //Debug.Log("Kettenblitz gecasted!");
    }
    

    // Update is called once per frame
    void Update(){}
    

    IEnumerator mainLoop(){
        while (numOfHits < maxTargets){
            int n = getIndexOfClosestNotYetBeenHitEnemy(); 
            //Wenn n == int.MaxValue, dann haben wir keinen Gegner den wir hitten können
            if(n == int.MaxValue){
                break;
            }
            drawLineToEnemy(n);
            hitEnemy(n);
            moveToEnemy(n);
            numOfHits++;
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

    void drawLineToEnemy(int targetIndex){
        GameObject target = targets[targetIndex];
        lr.positionCount = lr.positionCount + 1;
        lr.SetPosition(numOfHits+1, target.transform.position);
    }
    void hitEnemy(int targetIndex){
        GameObject enemy = targets[targetIndex];
        TestEnemy enemyScript = enemy.GetComponent<TestEnemy>();
        if(enemyScript == null){
            Debug.LogWarning("Kettenblitz hat einen Gegner gefunden, der nicht das erforderliche richtige Skript hat!");
            Debug.LogWarning("Alle Gegner mit den 'Enemy'-Tag müssen dieses Skript-Komponent haben. Wenn hier ein Fehler auftritt, bitte Moritz kontaktieren! :)");
            return;
        }
        enemyScript.takeDamage(DamageType.Blitz, damage);
        targetsHit[targetIndex] = true;
    }

    void moveToEnemy(int targetIndex){
        transform.position = targets[targetIndex].transform.position;
    }
    private int getIndexOfClosestNotYetBeenHitEnemy(){

        //Need to remember Distance and Index of closest Enemy
        //Wenn wir jemals 'int.MaxValue' Anzahl von Gegnern in unserem target-Array haben, 
        //haben wir ganz andere Probleme als dass dieser Code malfunctioned.
        float closestDistance = Mathf.Infinity;
        int closestIndex = int.MaxValue;

        for (int i = 0; i < targets.Length; i++) {
            //Check if target has already been hit, if yes, immediately continue.
            //Never hit same target twice.
            if (targetsHit[i]){
                continue;
            }
            //Check if current GameObject is closer than previous closest, if yes, remember new closest.
            GameObject g = targets[i];
            float currentDistance = Vector3.Distance(transform.position, g.transform.position);
            if (currentDistance < closestDistance && currentDistance < range){
                closestDistance = currentDistance;
                closestIndex = i;
            }
        }
        return closestIndex;

    }
}
