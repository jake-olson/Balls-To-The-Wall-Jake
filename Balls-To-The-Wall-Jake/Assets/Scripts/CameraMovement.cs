using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraMovement : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private CinemachineTransposer transposer;

    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x >= 6.9 || vcam.Follow.transform.position.x < 0 || transform.position.y <= 71 || transform.position.y > 80)
        {
            if (transposer.m_FollowOffset.x < 0)
            {
                transposer.m_FollowOffset = new Vector3(transposer.m_FollowOffset.x + 0.125f, 0, -10);
            }
            if (vcam.m_Lens.FieldOfView > 60f)
            {
                vcam.m_Lens.FieldOfView -= 0.5f;
            }
        }
        else if (transform.position.x <= 6.9 && transform.position.y >= 75.2)
        {
            if (transposer.m_FollowOffset.x > -8)
            {
                transposer.m_FollowOffset = new Vector3(transposer.m_FollowOffset.x - 0.125f, 0, -10);
            }
            if (vcam.m_Lens.FieldOfView < 90f)
            {
                vcam.m_Lens.FieldOfView += 0.5f;
            }
        }
        
    }
}
