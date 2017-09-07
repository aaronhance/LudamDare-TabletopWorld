using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct manCheck {
    public float distance;
    public GameObject man;
}

public class Zombie : MonoBehaviour {

    public Transform target;
    Man man;

    Pathfinding.Path path;
    private Seeker seeker;
    int currentWaypoint = 0;

    public CharacterController CharController;
    public Healthbar healthbar;

    public bool hasPath = false;
    public bool hasTarget = false;

    public float speed = 3f;

    public float timeCounter;
    public float timeCounter2;
    public float timeCounter2max;


    //audio
    AudioSource audiosource;

    public List<AudioClip> injuredAudio = new List<AudioClip>();
    public List<AudioClip> idleAudio = new List<AudioClip>();


    // Use this for initialization
    void Start () {
        seeker = GetComponent<Seeker>();
        CharController = this.GetComponent<CharacterController>();
        audiosource = GetComponent<AudioSource>();
        timeCounter2max = Random.Range(3, 20);
    }
	
	// Update is called once per frame
	void Update () {

        if (healthbar.dead) {
            Destroy(gameObject);
        }

        float distance;

        if (target == null) {
            getTarget();
            distance = 10;
        }
        else {
            distance = Vector3.Distance(transform.position, target.position);
        }

        //move

        if (distance < 3) {
            if (timeCounter > 2) {
                man.attack(Random.Range(1, 4), gameObject);
                timeCounter = 0;
            }
            timeCounter += Time.deltaTime;
        }

        if (timeCounter2 > timeCounter2max) {
            audiosource.clip = idleAudio[Random.Range(0, idleAudio.Count)];
            audiosource.Play();
            timeCounter2 = 0;
            timeCounter2max = Random.Range(3, 20);
        }
        timeCounter2 += Time.deltaTime;


        if (target == null) {
            getTarget();
            distance = 10;
        }

        if (!hasPath) {
            if (distance > 4) {
                seeker.StartPath(transform.position, target.position, OnPathComplete);
                hasTarget = true;
                hasPath = true;
            }
        }
        else {
            if (path == null) {
                hasPath = false;
                return;
            }

            if (currentWaypoint >= path.vectorPath.Count) {
                hasPath = false;
                currentWaypoint = 0;
                return;
            }

            //move on path

            Vector3 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized * speed;

            CharController.SimpleMove(direction);
            transform.rotation = Quaternion.LookRotation(direction);

            if (Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]) < 1.5f) {
                currentWaypoint++;
            }
        }
		
	}

    void getTarget() {

        List<manCheck> distances = new List<manCheck>();


        foreach (GameObject go in PlayerManager.singleton.men) {
            manCheck mc = new manCheck();
            mc.man = go;
            mc.distance = Vector3.Distance(transform.position, go.transform.position);
            Debug.Log("dfsdfafadaffdsf");
            distances.Add(mc);
            Debug.Log(mc.man.name);
        }

        if (distances.Count == 0) {
            Debug.Log("RRRRROOOROROROR");
            return;
        }

        distances = distances.OrderByDescending(o => o.distance).ToList();

        target = distances[0].man.transform;
        man = target.gameObject.GetComponent<Man>();

        Debug.Log(target.name);

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

    public void attack(float damage) {
        healthbar.damage(damage);
        audiosource.clip = injuredAudio[Random.Range(0, injuredAudio.Count - 1)];
        audiosource.Play();
    }

}
