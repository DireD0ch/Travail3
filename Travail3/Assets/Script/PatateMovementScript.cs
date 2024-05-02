using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatateMovementScript : MonoBehaviour
{
    public Transform Patate;
    public float distanceCurrentPatate;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Follow(Vector3 pos, float speed)
    {
        Vector3 position = pos - Vector3.forward * distanceCurrentPatate;
        Patate.position = Vector3.Lerp(Patate.position, position, Time.deltaTime * speed / distanceCurrentPatate);

    }
}
