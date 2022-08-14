using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletA : MonoBehaviour
{
    Rigidbody rb;
    bool isCollison;

    private void ActionTime()
    {
        transform.Translate(-Vector3.forward * Time.deltaTime * 100);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCollison) ActionTime();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy(gameObject);
        //print("hit");
        isCollison = true;
        
    }

}
