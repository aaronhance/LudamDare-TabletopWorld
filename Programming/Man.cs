using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man : MonoBehaviour {

    public Transform target;
    private Seeker seeker;
    public WorkItem currentWork;

    Pathfinding.Path path;

    private int currentWaypoint;

    public bool idle = true;
    public bool hasPath = false;
    public bool finishedPath = false;
    public bool finishedWork = false; //
    public bool inCombat = false;
    public float speed = 3;

    public float timeCounter;
    public float timeCounter2;

    CharacterController CharController;

    public float health;
    public Healthbar healthBar;

    //audio
    public AudioSource audiosource;
    public List<AudioClip> cuttingAudio = new List<AudioClip>();
    public AudioClip injuredAudio;
    public AudioClip fishingAudio;

    //inventory

    public bool wPick = false, wSword = false;
    public bool sPick = false, sSword = false;
    public bool fishingRod = false;

    public GameObject wpick, wsword, spick, ssword;

    GameObject equiped;

    //combat
    public GameObject attacker;

    //fshing
    public GameObject fishFloat;

    GameObject floatInst;
    public float fishtime;
    public float fishMaxTime;

    //house
    public House myHouse;



    // Use this for initialization
    void Start () {
        seeker = GetComponent<Seeker>();
        CharController = this.GetComponent<CharacterController>();
        audiosource = GetComponent<AudioSource>();
            //make path

    }

    public void cancelOrders() {
        idle = true;
        hasPath = false;
        finishedPath = false;
        currentWork = null;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        //inventory stuff

        if (!sPick) {
            if (PlayerManager.singleton.sPick > 0) {
                PlayerManager.singleton.sPick -= 1;
                sPick = true;
            }
            else {
                if (!wPick) {
                    if (PlayerManager.singleton.wPick > 0) {
                        PlayerManager.singleton.wPick -= 1;
                        wPick = true;
                    }
                }
            }
        }
        if (!sSword) {
            if (PlayerManager.singleton.sSword > 0) {
                PlayerManager.singleton.sSword -= 1;
                sSword = true;
            }
            else {
                if (!wSword) {
                    if (PlayerManager.singleton.wSword > 0) {
                        PlayerManager.singleton.wSword -= 1;
                        wSword = true;
                    }
                }
            }
        }

        //other shit
        //This source code is too messy for me to look at anymore,:'(

        if (myHouse == null) {
            Destroy(this.gameObject);
        }

        if (healthBar.dead) {
            PlayerManager.singleton.men.Remove(gameObject);
            Destroy(gameObject);
        }

        if (!CharController.isGrounded) {
            Vector3 pos = transform.position;
            CharController.Move(Vector3.down * 2 * Time.fixedDeltaTime);
            transform.position = new Vector3(pos.x, transform.position.y, pos.z);
        }

        if (inCombat) {

            if (attacker == null) {
                inCombat = false;
                Destroy(equiped);

                return;
            }

            Destroy(equiped);


            if (Vector3.Distance(transform.position, attacker.transform.position) < 4) {
                //setup damage on best weapon and equip
                float mult = 1;

                if (sSword) {
                    mult = 1.55f;
                    equiped = Instantiate(ssword, this.transform);
                    equiped.transform.localPosition = new Vector3(3, 2, 0.88f);
                    equiped.transform.localRotation = Quaternion.Euler(38.8f, 1f, 2.31f);
                }
                else if (wSword) {
                    equiped = Instantiate(wsword, this.transform);
                    equiped.transform.localPosition = new Vector3(3, 2, 0.88f);
                    equiped.transform.localRotation = Quaternion.Euler(38.8f, 1f, 2.31f);
                    mult = 1.3f;
                }

                if (timeCounter2 > 1.6f) {
                    attacker.GetComponent<Zombie>().attack(Random.Range(1, 4) * mult);
                    timeCounter2 = 0;
                }
                timeCounter2 += Time.fixedDeltaTime;

            }
        }

        if (!idle) {
            if (finishedPath) {

                Destroy(equiped);

                //do work TODO
                if (currentWork.type == "Tree") {

                    if (currentWork == null) {
                        idle = true;
                        finishedPath = false;
                        Destroy(equiped);
                        return;
                    }

                    float mult = 1;

                    if (sPick) {
                        mult = 1.55f;
                        equiped = Instantiate(spick, this.transform);
                        equiped.transform.localPosition = new Vector3(3, 2, 0.88f);
                        equiped.transform.localRotation = Quaternion.Euler(38.8f, -0.2f, 2.31f);
                    }
                    else if (wPick) {
                        equiped = Instantiate(wpick, this.transform);
                        equiped.transform.localPosition = new Vector3(3, 2, 0.88f);
                        equiped.transform.localRotation = Quaternion.Euler(38.8f, -0.2f, 2.31f);
                        mult = 1.3f;
                    }

                    timeCounter += Time.fixedDeltaTime;
                    if (timeCounter > 1) {
                        audiosource.clip = cuttingAudio[Random.Range(0, cuttingAudio.Count - 1)];
                        audiosource.Play();
                        timeCounter = 0;
                        CharController.Move(Vector3.up * 0.5f);
                        currentWork.gameObject.GetComponent<tree>().workRequired -= 1 * mult;
                    }
                }
                else if (currentWork.type == "Fish") {
                    if (currentWork == null) {
                        idle = true;
                        finishedPath = false;
                        return;
                    }

                    if (fishMaxTime == 0) {
                        fishMaxTime = Random.Range(5, 20);
                        //init float

                        floatInst = Instantiate(fishFloat, new Vector3(Random.Range(-2.76f, 5.46f), 34.74f, Random.Range(9.2f, 21.72f)), Quaternion.Euler(0, Random.Range(0, 360), 0));
                    }

                    fishtime += Time.fixedDeltaTime;
                    timeCounter += Time.fixedDeltaTime;

                    if (timeCounter > 2) {
                        audiosource.clip = fishingAudio;
                        audiosource.Play();
                        timeCounter = 0;
                    }
                    if (fishtime > fishMaxTime){
                        PlayerManager.singleton.fish += Random.Range(1, 3);
                        fishMaxTime = 0;
                        Destroy(floatInst);
                        currentWork = null;
                        idle = true;
                        finishedPath = false;
                        return;
                    }
                }
            }
            else {

                if (path == null) {
                    hasPath = false;
                    return;
                }

                if (currentWaypoint >= path.vectorPath.Count) {
                    finishedPath = true;
                    hasPath = false;
                    currentWaypoint = 0;
                    return;
                }

                //move on path

                Vector3 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized * speed;

                CharController.SimpleMove(direction);
                transform.rotation = Quaternion.LookRotation(direction);

                if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < 1.8f) {
                    currentWaypoint++;
                }
            }
        }
        else {

            if (healthBar.currentHealth < healthBar.maxHealth && PlayerManager.singleton.fish > 0) {
                PlayerManager.singleton.fish -= 1;
                healthBar.currentHealth += Random.Range(1, 2);
                if (healthBar.currentHealth > healthBar.maxHealth) healthBar.currentHealth = healthBar.maxHealth;
            }

            if (PlayerManager.singleton.workQueue.Count < 1) return;

            currentWork = PlayerManager.singleton.workQueue[0];
            PlayerManager.singleton.workQueue.Remove(currentWork);

            target = currentWork.transform;
            seeker.StartPath(transform.position, target.position, OnPathComplete);

            hasPath = true;
            idle = false;
        }
	}

    void OnPathComplete(Pathfinding.Path p) {

        if (p.error) {
            Debug.Log("Path Failed!");
            hasPath = false;
            return;
        }

        path = p;
        hasPath = true;
    }

    public void attack(int damage, GameObject attacker) {
        audiosource.clip = injuredAudio;
        audiosource.Play();
        healthBar.damage(damage);
        inCombat = true;
        this.attacker = attacker;
    }

}
