using Dreamteck.Forever;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Dreamteck.WelcomeWindow.WindowPanel;



public class PlayerMovement : MonoBehaviour
{
    public float ForceSaut = 10f;
    LaneRunner runner;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        runner = GetComponent<LaneRunner>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) runner.lane--;
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) runner.lane++;

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * ForceSaut, ForceMode.Impulse);
        }

    }
}
