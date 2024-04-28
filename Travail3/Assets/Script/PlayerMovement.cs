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

    //Variables pour la collecte de pièce
    public TMP_Text pieceTexte;
    public int score;

    //Variables pour les mouvemnts
    public float ForceSaut = 10f;
    LaneRunner runner;
    Rigidbody rb;
    public bool peutsauter = false;
    int augmenterVitesse = 0;

    //Variable pour les animations de des cameras
    public Camera cam;
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            nbVie--; // Enlève une vie 
        }

        if (collision.gameObject.CompareTag("Piece"))
        {
            score = score + 1;
            collision.gameObject.SetActive(false); // "Enlève" le diamant en question
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

        // Pour les déplacement entre les "lane" et sauter
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) runner.lane--;
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) runner.lane++;

        if ((Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && peutsauter == true) // On donne le choix au joueur de sauter avec la flèche du haut ou la barre espace
        {
            rb.AddForce(Vector3.up * ForceSaut, ForceMode.Impulse);
        }

        // Difficulter accrue le plus de piece rammaser
        if (score > 0 && score % 5 == 0) // Augmenter la vitesse tous les multiples de 5
        {
            // Incrémenter le compteur d'augmentation de vitesse
            augmenterVitesse++;

            // Augmenter la vitesse uniquement lorsque le compteur atteint un certain seuil
            if (augmenterVitesse >= 3) // Augmenter la vitesse tous les 3 scores de 5
            {
                runner.followSpeed += 0.01f;
                // Réinitialiser le compteur d'augmentation de vitesse
                augmenterVitesse = 0;
            }
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
        float moveSpeed = 2f; // vitesse de déplacement de la caméra

        
        cam.transform.position = Vector3.Lerp(cam.transform.position, targetPosition, Time.deltaTime * moveSpeed);
        yield return null;
        

        cam.transform.position = targetPosition;
        cam.transform.Rotate(180f, 0f, 0f, UnityEngine.Space.Self); // J'ai  du mettre "UnityEngine.Space.Self" parce que le "Forever plugin" a une propriété  "Space.Self" et ce n'est pas ce que je voulais 
    }
}
