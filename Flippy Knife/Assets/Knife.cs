using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Knife : MonoBehaviour {

    public Rigidbody rb;

    public float force = 5f;
    public float torque = 20f;

    private float time;

    private Vector2 startSwipe;
    private Vector2 endSwipe;
    private Vector2 swipe;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!rb.isKinematic)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            startSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            endSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Swipe();
        }
	}

    void Swipe()
    {
        rb.isKinematic = false;
        time = Time.time;
        swipe = endSwipe - startSwipe;
        swipe = new Vector2(-swipe.x,swipe.y);
        rb.AddForce(swipe * force,ForceMode.Impulse);
        rb.AddTorque(0f,0f,torque,ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Block")
            rb.isKinematic = true;
        else Restart();
    }
    private void OnCollisionEnter(Collision collision)
    {
        float timeInAir = Time.time - time;

        if (!rb.isKinematic && timeInAir> 0.1f)
        {
            Restart();
        }
    }
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
