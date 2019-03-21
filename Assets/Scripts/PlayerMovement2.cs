using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement2 : MonoBehaviour 
{
    public float moveSpeed = 12f;
    public float rayDistance = 5f;
    public Text countText;
    public Text winText;
    public Camera cam;
    public GameObject leftCheck;
    public GameObject rightCheck;
    public GameObject restartButton;
 
 
    private Rigidbody playerRb;
    private Transform playerRotation;
    private int count;
    private float keyDelay = .4f;
    private float timePassed;
    private bool keepMove;
    private LeftTurnCheck leftTurnCheck;
    private RightTurnCheck rightTurnCheck;

 
    // Use this for initialization
    void Awake()
    {
        playerRotation = GetComponent<Transform>();
        playerRb = GetComponent<Rigidbody>();
        leftTurnCheck = leftCheck.GetComponent<LeftTurnCheck>();
        rightTurnCheck = rightCheck.GetComponent<RightTurnCheck>();
    }
    void Start()
    {
        count = 0;
        SetCountText();
        winText.text = "";
        restartButton.SetActive(false);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
       
        ContinuousMovement();
    }
 
    void Update()
    {
        RaycastHit hit;
        Ray forwardRay = new Ray (transform.position, transform.forward);
        Debug.DrawRay(transform.position, transform.forward*rayDistance, Color.blue);
        {
            if (Physics.Raycast(forwardRay, out hit, rayDistance))
            {
                if(hit.collider.tag == "Pac1")
                {
                    Destroy(hit.collider.gameObject);
                    P2Win();
                    cam.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
                }
                if(hit.collider.tag == "Wall")
                {
                    keepMove = false;
                }
                else
                {
                    keepMove = true;
                }
            }
            else if(keepMove == false)
            {
                keepMove = true;
            }
        }
        timePassed += Time.deltaTime;
        if (timePassed >= keyDelay && Input.GetKeyDown(KeyCode.A) && leftTurnCheck.leftTurnable == true)
        {      
            playerRotation.Rotate(0, -90f, 0);
            timePassed = 0f;             
        }
        if (timePassed >= keyDelay && Input.GetKeyDown(KeyCode.D) && rightTurnCheck.rightTurnable == true)
        {
            playerRotation.Rotate(0, 90f, 0);
            timePassed = 0f;
        }
    
        
    }
 
    public void ContinuousMovement()
    {
        if (keepMove)
        {
            playerRb.MovePosition(transform.position + transform.forward * moveSpeed * Time.deltaTime);    
        }     
       
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag ( "Collectibles"))
        {
            other.gameObject.SetActive (false);
            count = count + 1;
            SetCountText();
        }
        if(other.gameObject.CompareTag ( "Powerups"))
        {
            moveSpeed += 0.5f;
        }
    }
    void SetCountText()
    {
        countText.text = "Pac2 Score: " + count.ToString();
    }
    void P2Win()
    {
        winText.text = "Pac2 Wins!";
        restartButton.SetActive(true);
    }
}
