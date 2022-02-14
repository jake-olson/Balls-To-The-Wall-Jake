using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Material shadedMaterial;
    [SerializeField] float maxLineLength;
    public bool launched;
    private Vector3 ballPosition;

    // Start is called before the first frame update
    void Start()
    {
        launched = false;
        ballPosition = transform.position;
    }
    
    // Update is called once per frame
    void Update()
    {
        // Manages position of cue ball
        if (!launched)
        {
            Vector3 v3 = Input.mousePosition;
            v3.z = 10f;
            v3 = Camera.main.ScreenToWorldPoint(v3);
            Vector3 length = ballPosition - v3;
            if (length.sqrMagnitude < maxLineLength * maxLineLength)
            {
                transform.position = v3;

            } else
            {
                float ratio = (length.magnitude - (maxLineLength)) / (length.magnitude);
                float xPosition = v3.x + length.x * (ratio);
                float yPosition = v3.y + length.y * (ratio);
                transform.position = new Vector3(xPosition, yPosition, 0);
            }
        }
        // launched is true if mousebutton is released while it hasn't been launched yet
        if (!launched && Input.GetMouseButtonUp(0)) {
            launched = true;
        }
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "ball" && launched)
        {
            Destroy(gameObject);
        }
    }
}
