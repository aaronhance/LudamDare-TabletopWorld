using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour {
    PlayerManager pm;
	public Text Mode;

    private void Start() {
        pm = GetComponent<PlayerManager>();
		treeChop ();
    }

    public void treeChop() {
        pm.buildPanel.SetActive(false);
        pm.craftPanel.SetActive(false);

		pm.crafting = false;
        pm.treeChopping = true;
        pm.building = false;

		Mode.text = "Control Mode";

        Destroy(pm.shadowObject);
    }

    public void build() {
        pm.buildPanel.SetActive(true);
        pm.craftPanel.SetActive(false);

		pm.crafting = false;
		pm.treeChopping = false;
		pm.building = true;

		Mode.text = "Build Mode";

		Destroy (pm.shadowObject);
    }

    //build objects

    public void buildFire() {
        pm.building = true;
        pm.setCurrentObject("Fire");
    }
    public void buildSapling() {
        pm.building = true;
        pm.setCurrentObject("Sapling");
    }
    public void buildWoodHouse() {
        pm.building = true;
        pm.setCurrentObject("Wood House");
    }

    //craft
    public void craft() {
		pm.buildPanel.SetActive(false);
		pm.craftPanel.SetActive(true);

		pm.crafting = true;
		pm.treeChopping = false;
		pm.building = false;

		Mode.text = "Craft Mode";

        Destroy(pm.shadowObject);
    }

    public void craftWPick() {
        //take res, add pick
        PlayerManager.singleton.wood -= 4;
        PlayerManager.singleton.wPick += 1;
    }

    public void craftSPick() {
        PlayerManager.singleton.stone -= 2;
        PlayerManager.singleton.wood -= 2;
        PlayerManager.singleton.sPick += 1;
    }


    public void craftWSword() {
        PlayerManager.singleton.wood -= 4;
        PlayerManager.singleton.wSword += 1;

    }

    public void craftSSword() {
        PlayerManager.singleton.stone -= 2;
        PlayerManager.singleton.wood -= 2;
        PlayerManager.singleton.sSword += 1;
    }




    public void play() {
        Application.LoadLevel(1);
    }

    public void exit() {
        Application.Quit();
    }

    public void cancelOrders() {
        pm.cancelAllOrders();
    }
}
