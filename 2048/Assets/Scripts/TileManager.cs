using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public GameObject[] tilePositions;

    private TileCreator tc;
    private GameObject[] tileObjects;
    private List<TileClass> tileObjectList;
    private int[] tiles;

	// Use this for initialization
	void Start () {
        tiles = new int[9];
        tc = this.GetComponent<TileCreator>();
        tileObjectList = new List<TileClass>();

        for(int i = 0; i < tiles.Length; i++) {
            tiles[i] = 0;
        }

        tileObjects = new GameObject[9];

        createTileValue();
        createTileValue();

        //tileObjects[0] = tileObjects[1];
        //tileObjects[1] = null;

        //for(int i = 0; i < tileObjects.Length; i++) {
        //    Debug.Log("array list :  " + tileObjects[i]);
        //}
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKey(KeyCode.RightArrow)) {
            checkMoveTile("right");
        }
	}

    void createTileValue() {
        int temp = setRandom();
        GameObject gtemp = null;

        while(!checkCreatableTile(temp)) {
            temp = setRandom();
        }

        this.tiles[temp] = 2;
        
        gtemp = tc.createTile(2, getTilePosition(temp));

        tileObjectList.Add(new TileClass(gtemp, temp));
    }

    void createTile(int value, int target_position) {
        Vector3 vtemp = findTilePositon(target_position);
        GameObject gtemp = tc.createTile(value, vtemp);
        tileObjectList.Add(new TileClass(gtemp, target_position));
    }

    int setRandom() {
        int rand = Random.Range(0, 8);

        return rand;
    }

    bool checkCreatableTile(int num) {
        if(this.tiles[num] == 0) {
            return true;
        }

        return false;
    }

    Vector3 getTilePosition(int number) {
        Vector3 value = tilePositions[number].transform.position;

        return value;
    }
    
    void checkMoveTile(string direction) {
        switch(direction) {//012 345 678
            case "right":
                for(int i = 0; i < 9; i++) {
                    if(i % 3 != 2 && tiles[i] != 0) {
                        if(tiles[i+1] == 0) {
                            if(i % 3 == 0 && tiles[i+2] == 0) {
                                moveTile(i, i + 2);
                            } else {
                                moveTile(i, i + 1);
                            }
                        } else {
                            if(checkEquleTile(i, i + 1)) {
                                combineTile(i, i + 1);
                            } else if(i % 3 == 0 && tiles[i + 1] == 0 && checkEquleTile(i, i + 2)) {
                                combineTile(i, i + 2);
                            }
                        }
                    }
                }
                break;
            case "left":
                break;
            case "up":
                break;
            case "down":
                break;
        }
    }

    void moveTile(int current, int target) {
        tiles[target] = tiles[current];
        tiles[current] = 0;
        Debug.Log("current : " + current + "target : " + target);
        GameObject ctemp = findTileClass(current).getTile();
        ctemp.transform.position = getTilePosition(target); // move (object)tile
    }

    void combineTile(int current, int target) {
        //Vector3 temp = findTilePositon(target);


        tiles[target] += tiles[current];
        tiles[current] = 0;

        createTile(tiles[target], target);

        deleteTile(current);
        deleteTile(target);
    }

    bool checkEquleTile(int current, int target) {
        if(tiles[current] == tiles[target]) {
            return true;
        } 
        return false;
    }

    void deleteTile(int target) {
        Debug.Log("target : " + target);
        TileClass temp = findTileClass(target);
        temp.destroyTile();
        tileObjectList.Remove(temp);
    }

    TileClass findTileClass(int target_number) {
        for(int i = 0; i < tileObjectList.Count; i++) {
            if(tileObjectList[i].getPosition() == target_number) {
                return tileObjectList[i];
            }
        }
        return null;
    }

    Vector3 findTilePositon(int target_number) {
        return findTileClass(target_number).getTile().transform.position;
    }

    class TileClass {
        GameObject tile;
        int position;

        public TileClass(GameObject value, int num) {
            this.tile = value;
            this.position = num;
        }

        public int getPosition() {
            return position;
        }

        public GameObject getTile() {
            return tile;
        }

        void setTilePosition(Vector3 value) {
            this.tile.transform.position = value;
        }

        public void destroyTile() {
            Destroy(this.tile);
        }
    }
}

