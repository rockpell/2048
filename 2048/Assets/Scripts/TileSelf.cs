using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSelf : MonoBehaviour {

    Vector3 completeScale = new Vector3(1, 1, 1);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(!checkScale()) {
            Vector3 temp = this.transform.localScale;
            
            this.transform.localScale = new Vector3(temp.x + 0.02f, temp.y + 0.02f, 1);
        }
	}

    void setScaleUp() {

    }

    bool checkScale() {
        if(this.transform.localScale.x >= completeScale.x){
            return true;
        }
        return false;
    }
}
