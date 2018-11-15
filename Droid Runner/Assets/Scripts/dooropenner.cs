using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class dooropenner : MonoBehaviour {
    Animator animator;
    public Text t;
    float score = 0f;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        score++;
        animator.SetTrigger("OpenDoor");
        t.text = " Triggered " +score;
    }

    private void OnTriggerExit(Collider other)
    {
        animator.SetTrigger("CloseDoor");
        t.text = "Not Triggered";
    }
}
