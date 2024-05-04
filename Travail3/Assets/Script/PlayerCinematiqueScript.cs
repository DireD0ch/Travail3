using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCinematicAnimation : MonoBehaviour
{
    // Variable 
    public float forceCourir = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Déplacement vers l'avant en utilisant la force
        transform.Translate(Vector3.forward * forceCourir * Time.deltaTime);
    }
    
}
