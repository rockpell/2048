using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {

    public GameObject[] tilePositions;

    private TileCreator tc;
    private List<TileClass> tileObjectList;
    private int[] tiles;

    private bool creatable_init_tile = false;

    private int col = 3;

	// Use this for initialization
	void Start () {
        tiles = new int[9];
        tc = this.GetComponent<TileCreator>();
        tileObjectList = new List<TileClass>();

        for(int i = 0; i < tiles.Length; i++) {
            tiles[i] = 0;
        }

        createInitTile();
        createInitTile();
        createInitTile();
        createInitTile();
        createInitTile();
    }
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.RightArrow)) {
            checkMove("right");
            checkCreateInitTile();
        } else if(Input.GetKeyDown(KeyCode.LeftArrow)) {
            checkMove("left");
            checkCreateInitTile();
        } else if(Input.GetKeyDown(KeyCode.UpArrow)) {
            checkMoveTile("up");
            checkCreateInitTile();
        } else if(Input.GetKeyDown(KeyCode.DownArrow)) {
            checkMoveTile("down");
            checkCreateInitTile();
        }
    }

    void checkCreateInitTile() {
        if(creatable_init_tile) {
            createInitTile();
            creatable_init_tile = false;
        }
    }

    void createInitTile() {
        int temp = setRandom();
        GameObject gtemp = null;

        while(!checkCreatableTile(temp)) {
            if(isFullTile()) {
                return;
            }
            temp = setRandom();
        }

        this.tiles[temp] = 2;
        
        gtemp = tc.createTile(2, getTilePosition(temp));
        tileObjectList.Add(new TileClass(gtemp, temp, 2));
    }

    bool isFullTile() {
        for(int i = 0; i < tiles.Length; i++) {
            if(tiles[i] == 0) {
                return false;
            }
        }

        return true;
    }

    void createTile(int value, int target_position) {
        Vector3 vtemp = findTargetTilePositon(target_position);
        GameObject gtemp = tc.createTile(value, vtemp);
        tileObjectList.Add(new TileClass(gtemp, target_position, value));
        Debug.Log("createTile");
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
                    if(i % col != 2 && tiles[i] != 0) {
                        if(tiles[i + 1] == 0) {
                            if(i % col == 0 && tiles[i + 2] == 0) {
                                moveTile(i, i + 2);
                            } else {
                                moveTile(i, i + 1);
                            }
                        } else {
                            if(checkEquleTile(i, i + 1)) {
                                combineTile(i, i + 1);
                            } else if(i % col == 0 && tiles[i + 1] == 0 && checkEquleTile(i, i + 2)) {
                                combineTile(i, i + 2);
                            }
                        }
                    }
                }
                break;
            case "left":
                for(int i = 0; i < 9; i++) {
                    if(i % col != 0 && tiles[i] != 0) {
                        if(tiles[i - 1] == 0) {
                            if(i % col == 2 && tiles[i - 2] == 0) {
                                moveTile(i, i - 2);
                            } else {
                                moveTile(i, i - 1);
                            }
                        } else {
                            if(checkEquleTile(i, i - 1)) {
                                combineTile(i, i - 1);
                            } else if(i % col == 2 && tiles[i - 1] == 0 && checkEquleTile(i, i - 2)) {
                                combineTile(i, i - 2);
                            }
                        }
                    }
                }
                break;
            case "up":
                for(int i = 0; i < 9; i++) {
                    if(i >= col && tiles[i] != 0) {
                        if(tiles[i - 1* col] == 0) {
                            if(i >= 2*col && tiles[i - 2* col] == 0) {
                                moveTile(i, i - 2* col);
                            } else {
                                moveTile(i, i - 1* col);
                            }
                        } else {
                            if(checkEquleTile(i, i - 1* col)) {
                                combineTile(i, i - 1* col);
                            } else if(i >= 2*col && tiles[i - 1*col] == 0 && checkEquleTile(i, i - 2* col)) {
                                combineTile(i, i - 2);
                            }
                        }
                    }
                }
                break;
            case "down":
                for(int i = 0; i < 9; i++) {
                    if(i < 2 * col && tiles[i] != 0) {
                        if(tiles[i + 1 * col] == 0) {
                            if(i < col && tiles[i + 2 * col] == 0) {
                                moveTile(i, i + 2 * col);
                            } else {
                                moveTile(i, i + 1 * col);
                            }
                        } else {
                            if(checkEquleTile(i, i + 1 * col)) {
                                combineTile(i, i + 1 * col);
                            } else if(i < col && tiles[i + 1 * col] == 0 && checkEquleTile(i, i + 2 * col)) {
                                combineTile(i, i + 2 * col);
                            }
                        }
                    }
                }
                break;
        }
    }

    void checkMove(string direction) {
        if(direction == "left") {
            Queue<TileClass> queue = new Queue<TileClass>();
            int[,] temp_arry = getTileArray();

            for(int i = 0; i < col; i++) {
                for(int p = 0; p < col; p++) {
                    if(temp_arry[i, p] != 0) {
                        queue.Enqueue(findTileClass(i));
                    }
                    Debug.Log(i+"  :   "+p);
                }
                
            }

            while(queue.Count > 0) {
                TileClass temp_tile = queue.Dequeue();

                
            }


        } else if(direction == "right") {

        }
        
    }

    void moveTile(int current, int target) {
        tiles[target] = tiles[current];
        tiles[current] = 0;

        TileClass ct = findTileClass(current);
        GameObject ctemp = ct.getTile();

        ct.movePosition(target); // set move position
        ctemp.transform.position = getTilePosition(target); // move (object)tile

        creatable_init_tile = true; // 초기 타일 생성 가능
    }

    void combineTile(int current, int target) {

        tiles[target] += tiles[current];
        tiles[current] = 0;
        Debug.Log("current : " + current + "target : " + target);
        createTile(tiles[target], target);

        deleteTile(current);
        deleteTile(target);

        creatable_init_tile = true; // 초기 타일 생성 가능
    }

    bool checkEquleTile(int current, int target) {
        if(tiles[current] == tiles[target]) {
            return true;
        } 
        return false;
    }

    void deleteTile(int target) {
        //Debug.Log("target : " + target);
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

    Vector3 findTargetTilePositon(int target_number) {
        return findTileClass(target_number).getTile().transform.position;
    }

    int[,] getTileArray() {
        int[, ] result = new int[col , col];

        for(int i = 0; i < col; i++) {
            for(int p = 0; p < col; p++) {
                result[i, p] = 0;
            }
        }
        
        for(int i = 0; i < tileObjectList.Count; i++) {
            int temp = tileObjectList[i].getPosition();
            result[temp / col, temp % col] = tileObjectList[i].getValue();
        }

        return result;
    }

    class TileClass {
        GameObject tile;
        int position;
        int value;

        public TileClass(GameObject value, int num) {
            this.tile = value;
            this.position = num;
        }

        public TileClass(GameObject go, int num, int value) {
            this.tile = go;
            this.position = num;
            this.value = value;
        }

        public int getValue() {
            return this.value;
        }

        public void addValue() {
            this.value += this.value;
        }

        public int getPosition() {
            return position;
        }

        public void movePosition(int value) {
            this.position = value;
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

