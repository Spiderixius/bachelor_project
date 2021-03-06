﻿using UnityEngine;
using System.Collections;

public class TheDestroyer : MonoBehaviour {

    public float speed;
	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        rb.isKinematic = false;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.transform.position.y < -30)
        {
            Destroy(other.gameObject);
        }
    }

}
