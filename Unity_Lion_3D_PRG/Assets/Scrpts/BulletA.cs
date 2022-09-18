using System.Collections;
using System.Collections.Generic;
using agi;
using UnityEngine;
using UnityEngine.Rendering.VirtualTexturing;

public class BulletA : MonoBehaviour
{
    Rigidbody rb;
    AttackController ACsystem;
    bool isCollison;

    private void ActionTime()
    {
        transform.Translate(-Vector3.forward * Time.deltaTime * 100);
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ACsystem = FindObjectOfType<AttackController>();
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
        try
        {
            ACsystem.DmgCheck(collision.gameObject, 1);            
        }
        catch (System.Exception) { }
        isCollison = true;
        Destroy(gameObject);
    }

}
