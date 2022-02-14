using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingMesh : MonoBehaviour
{
    private Material planeMaterial;
    [SerializeField] float transparencyLevel;
    // Start is called before the first frame update
    void Start()
    {
        planeMaterial = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


   

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ball")
        {
            SetAlpha(transparencyLevel);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ball")
        {
            SetAlpha(1f);
        }
    }

    void SetAlpha(float alpha)
    {
        // Here you assign a color to the referenced material,
        // changing the color of your renderer
        Color color = planeMaterial.color;
        color.a = Mathf.Clamp(alpha, 0, 1);
        planeMaterial.color = color;
    }
}