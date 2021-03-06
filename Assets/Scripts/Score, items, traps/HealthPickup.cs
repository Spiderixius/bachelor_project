﻿using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour {

    public int healthAmountToGive;

    private LevelManager theLevelManager;

	// Use this for initialization
	void Start () {
        theLevelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            theLevelManager.AddHealth(healthAmountToGive);
            Destroy(gameObject);
        }
    }
}
