using Dreamteck.Forever;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using static Dreamteck.WelcomeWindow.WindowPanel;



public class PlayerMovement : MonoBehaviour
{
    //Variables pour la gestions des vies
    public int nbVie = 3;

    //Variables pour la collecte de pi�ce
    public TMP_Text pieceTexte;
    public int score;

    //Variables pour les mouvemnts
    public float ForceSaut = 10f;
    LaneRunner runner;
    Rigidbody rb;
    public bool peutsauter = false;

    //Variable pour les animations de des cameras
    public Camera cam;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            nbVie--; // Enl�ve une vie 
        }

        if (collision.gameObject.CompareTag("Piece"))
        {
            score = score + 1;
            collision.gameObject.SetActive(false); // "Enl�ve" le diamant en question
            pieceTexte.text = "Score : " + score.ToString();
        }

        if (collision.gameObject.CompareTag("Plancher"))
        {
            peutsauter = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plancher"))
        {
            peutsauter = false;
        }
    }
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

        if (Input.GetKey(KeyCode.Space) && peutsauter == true)
        {
            rb.AddForce(Vector3.up * ForceSaut, ForceMode.Impulse);
        }

        if (nbVie == 0) 
        {
            runner.follow = false;
            StartCoroutine(MoveCamera());

        }
    }
    IEnumerator MoveCamera()
    {
        Vector3 targetPosition = transform.position + new Vector3(0f, 10f, 0f); // position cible au-dessus du joueur
        float moveSpeed = 2f; // vitesse de d�placement de la cam�ra

        
        cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, Time.deltaTime * moveSpeed);
        yield return null;
        

        cam.transform.position = targetPosition;
        cam.transform.Rotate(180f, 0f, 0f, UnityEngine.Space.Self); // J'ai  du mettre "UnityEngine.Space.Self" parce que le "Forever plugin" a une propri�t�  "Space.Self" et ce n'est pas ce que je voulais 
    }
}
