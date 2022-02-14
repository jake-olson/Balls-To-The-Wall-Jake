using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Class that controls the ball's movement and related attributes
public class BallMovement : MonoBehaviour
{

    [SerializeField] float launchMultiplier;
    [SerializeField] float sizeScale;
    [SerializeField] GameObject CueBall;
    [SerializeField] float cueSize;
    [SerializeField] GameObject linkedWall;
    [SerializeField] Text winText;
    [SerializeField] Transform startPosition;
    private GameObject cueBall;
    private Vector3 clickPosition;
    private Vector3 unclickPosition;
    private Vector3 resetPosition;
    private Rigidbody rbd;
    private Rigidbody cueBody;
    private bool stuck;
    private Vector3 number;
    private bool cueColliding;
    private bool cheatsAllowed;



    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody>();
        stuck = false;
        transform.localScale = Vector3.one * sizeScale;
        resetPosition = startPosition.position;
        cheatsAllowed = false;
        winText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (stuck) {
            rbd.velocity = Vector3.zero;
        }
        if (transform.position.y < -50)
        {
            transform.position = resetPosition;
        }
        if (transform.position.y > 80 && transform.position.x > 10.5f)
        {
            winText.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            cheatsAllowed = !cheatsAllowed;
        }

        // Manages access to cheat checkpoints
        if (cheatsAllowed)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rbd.velocity = Vector3.zero;
                transform.position = new Vector3(8.59f, 2.96f, 0);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                rbd.velocity = Vector3.zero;
                transform.position = new Vector3(-7.7f, 24.34256f, 0);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                rbd.velocity = Vector3.zero;
                transform.position = new Vector3(6.1f, 25.675f, 0);
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                rbd.velocity = Vector3.zero;
                transform.position = new Vector3(13.17f, 15.857f, 0);
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                rbd.velocity = Vector3.zero;
                transform.position = new Vector3(27.53f, 36.18997f, 0);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                rbd.velocity = Vector3.zero;
                transform.position = new Vector3(17.3f, 36.6f, 0);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                rbd.velocity = Vector3.zero;
                transform.position = new Vector3(-11.87722f, 50.51713f, 0);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                rbd.velocity = Vector3.zero;
                transform.position = new Vector3(16.209f, 50.5f, 0);
                Destroy(linkedWall);
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                rbd.velocity = Vector3.zero;
                transform.position = new Vector3(6.6f, 75.284f, 0);
            }
        }

    }

    void OnMouseDown() {
        if (stuck) {
            clickPosition = transform.position;
            Debug.Log(clickPosition);
            cueBall = Instantiate(CueBall, clickPosition, Quaternion.identity);
            cueBall.transform.localScale = Vector3.one * cueSize;
            cueBody = cueBall.GetComponent<Rigidbody>();
        }
    }

    void OnMouseUp() {

        if (stuck) {

            if (!cueColliding) 
            {
                // Calculates Ball vector
                Vector3 v3 = Input.mousePosition;
                v3.z = 10f;
                v3 = Camera.main.ScreenToWorldPoint(v3);
                unclickPosition = v3;
                Debug.Log(clickPosition);

                Vector3 launchVector = (clickPosition - cueBall.transform.position) / 1;
                launchVector.z = 0;

                // Launches Cue Ball
                number = launchVector * launchMultiplier;
                cueBody.AddForce(number);
            } 
            else 
            {
                Destroy(cueBall);
            }
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("collision");
        if (collision.gameObject.tag == "stickyWall" || collision.gameObject.tag == "checkpointWall") {
        {
            stuck = true;
            rbd.useGravity = false;
            rbd.angularVelocity = Vector3.zero;

            int collisionLength = collision.contacts.Length;
            
            float totalX = 0;
            float totalY = 0;
            foreach (ContactPoint contactPoint in collision.contacts) {
                Vector3 point = contactPoint.point;
                totalX += point.x;
                totalY += point.y;
            }
            Vector3 midpoint = new Vector3(totalX / collisionLength, totalY / collisionLength, transform.position.z);

            Vector3 ballToMidPoint = transform.position - midpoint;
            transform.position = midpoint + ballToMidPoint.normalized * (sizeScale / 2);
        }

        }

        //Checks collisions with the ball
        if (collision.gameObject.tag == "checkpointWall")
        {
            rbd.velocity = Vector3.zero;
            if (collision.gameObject.name == "Cube (4)")
            {
                resetPosition = new Vector3(8.59f, 2.96f, 0);
            }
            if (collision.gameObject.name == "Cube (6)")
            {
                resetPosition = new Vector3(-7.7f, 24.34256f, 0);
            }
            if (collision.gameObject.name == "Cube (11)")
            {
                resetPosition = new Vector3(13.17f, 15.857f, 0);
            }
            if (collision.gameObject.name == "Cube (20)")
            {
                resetPosition = new Vector3(27.53f, 36.18997f, 0);
            }
            if (collision.gameObject.name == "Cube (20)")
            {
                resetPosition = new Vector3(17.3f, 36.6f, 0);
            }
            if (collision.gameObject.name == "Cube (5)")
            {
                resetPosition = new Vector3(-11.87722f, 50.51713f, 0);
            }
            if (collision.gameObject.name == "StickyWall")
            {
                resetPosition = new Vector3(8.085f, -39.737f, 0);
            }
        }
        if (collision.gameObject.tag == "destroyWall")
        {
            rbd.velocity = Vector3.zero;
            transform.position = resetPosition;
        }
        if (collision.gameObject.name == "LinkedWall")
        {
            Destroy(linkedWall);
        }
        if (collision.gameObject.tag == "inelasticWall")
        {
            rbd.velocity = new Vector3(0, rbd.velocity.y, rbd.velocity.z);
        }
    }

    void OnTriggerEnter(Collider collision) { 
        if (collision.gameObject.tag == "cueBall") cueColliding = true;
        if (collision.gameObject.tag == "cueBall" && cueBall.GetComponent<Movement>().launched)
        {
            // Launches regular Ball
            
            stuck = false;
            rbd.AddForce(number);
            rbd.useGravity = true;
        }
        if (collision.gameObject.tag == "arrow")
        {
            Vector3 arrowPosition = collision.gameObject.transform.position;
            rbd.position = arrowPosition;  //new Vector3(rbd.position.x, 1f, rbd.position.z);
            rbd.velocity = collision.gameObject.transform.up * 28;
            Debug.Log("Arrow");
        }
    }

    void OnTriggerExit(Collider col) {
        if (col.gameObject.tag == "cueBall") cueColliding = false;
    }
}
