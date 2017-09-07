using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct buildObject {
    public GameObject gameObject;
    public string name;
}

public class PlayerManager : MonoBehaviour {

    public bool treeChopping = false;
    public bool building = false;
    public bool crafting = false;

    public int wood = 0;
    public int stone = 0;
    public int people = 1;
    public int saplings = 0;
    public int fish = 2;

    public int wPick = 0;
    public int sPick = 0;
    public int wSword = 1;
    public int sSword = 0;

    public List<GameObject> builldObjectsRaw = new List<GameObject>();
    private List<buildObject> buildObjects = new List<buildObject>();

    public GameObject currentObject;
    public WorldObject shadowWorldObject;
    public GameObject shadowObject;

    public static PlayerManager singleton;

    public Camera cameraObject;

    public List<GameObject> houses = new List<GameObject>();
    public List<GameObject> men = new List<GameObject>();

    public List<WorkItem> workQueue = new List<WorkItem>();

    public Text peopleTxt;
    public Text woodTxt;
    public Text stoneTxt;
    public Text saplingsText;
    public Text fishText;

    public Text woodCost;
    public Text stoneCost;
    public Text saplingCost;

    public GameObject buildPanel;
    public GameObject craftPanel;

    public GameObject man;

    public GameObject emptyObject;

    // Use this for initialization
    void Start () {
        if (singleton == null) {
            singleton = this;
        }
        else {
            Destroy(this);
            return;
        }

            // populate build objects
            foreach (GameObject go in builldObjectsRaw) {
                WorldObject wo = go.GetComponent<WorldObject>();
                buildObject bo;
                bo.name = wo.name;
                bo.gameObject = go;
                buildObjects.Add(bo);
            }

        buildPanel.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        peopleTxt.text = "People: " + people;
        woodTxt.text = "Wood: " + wood;
        stoneTxt.text = "Stone: " + stone;
        saplingsText.text = "Saplings: " + saplings;
        fishText.text = "fish: " + fish;

        RaycastHit hit;

        if (building) {

            woodCost.text = "Wood: " + shadowWorldObject.woodCost;
            stoneCost.text = "Stone: " + shadowWorldObject.stoneCost;
            saplingCost.text = "Sapling: " + shadowWorldObject.saplingCost;

            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                if (Physics.Raycast(cameraObject.ScreenPointToRay(Input.mousePosition), out hit)) {
                    //TODO buildObject
                    if (hit.collider.gameObject.name == "board" && wood >= shadowWorldObject.woodCost && stone >= shadowWorldObject.stoneCost && saplings >= shadowWorldObject.saplingCost) {

                        GameObject go = Instantiate(currentObject, shadowObject.transform.position, shadowObject.transform.rotation);
                        stone -= shadowWorldObject.stoneCost;
                        wood -= shadowWorldObject.woodCost;
                        saplings -= shadowWorldObject.saplingCost;

                        WorldObject wo = go.GetComponent<WorldObject>();
                        AstarPath.active.Scan();
                        if (wo.type == "house") {
                            houses.Add(go);
                        }

                    }

                }
            }
            else {

                if (Input.GetKeyDown(KeyCode.Q)) {
                    shadowObject.transform.Rotate(new Vector3(0, -45, 0));
                }
                if (Input.GetKeyDown(KeyCode.E)) {
                    shadowObject.transform.Rotate(new Vector3(0, 45, 0));
                }

                if (Physics.Raycast(cameraObject.ScreenPointToRay(Input.mousePosition), out hit)) {
                    shadowObject.transform.position = hit.point;
                }
            }

        }
        else if (treeChopping) {
            if (Input.GetKeyDown(KeyCode.Mouse0)) { //TODO make sure it doesn't alreay have a work item
                if (Physics.Raycast(cameraObject.ScreenPointToRay(Input.mousePosition), out hit)) {
                    WorldObject wo = hit.collider.gameObject.GetComponent<WorldObject>();

                    if (wo != null && wo.name == "Tree" || wo != null && wo.name == "Stone") {
                        wo.gameObject.AddComponent<WorkItem>();
                        WorkItem wi = wo.gameObject.GetComponent<WorkItem>();
                        wi.type = "Tree";
                        workQueue.Add(wi);
                    }
                    else if (wo != null && wo.name == "Fishing") {
                        GameObject go = Instantiate(emptyObject, wo.transform.parent.gameObject.GetComponent<Fishing>().getSpot().position, Quaternion.identity);
                        WorkItem wi = go.AddComponent<WorkItem>();
                        wi.type = "Fish";
                        workQueue.Add(wi);
                    }
                }
            }
        }
	}

    public void setCurrentObject(string name) {
        currentObject = buildObjects.Find(item => item.name == name).gameObject;
        Destroy(shadowObject);
        shadowObject = Instantiate(currentObject, Vector3.zero, Quaternion.identity);
        shadowWorldObject = shadowObject.GetComponent<WorldObject>();
        shadowObject.GetComponent<Collider>().enabled = false;
    }

    public void cancelAllOrders() {
        workQueue.Clear();

        foreach (GameObject m in men) {
            m.GetComponent<Man>().cancelOrders();
        }
    }

}
