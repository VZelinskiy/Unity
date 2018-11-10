using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBrick : MonoBehaviour {
    Stats stats;

	// Use this for initialization
	void Start () {
        stats = GetComponent<Stats>();
	}
	
	public void TakeDamage (float amount) {
        stats.Health -= amount;

        if (stats.Health <= 0)
        {
            Destroy(gameObject);
        }
	}


}
