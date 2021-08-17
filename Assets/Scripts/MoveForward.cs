using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public bool isGrounded;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float Gravity = -20;
    public Transform PlayerrTrasform;


    private Vector3 direction;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        PlayerrTrasform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.isGameStarted)
            return;

        transform.Translate(Vector3.forward * Time.deltaTime * speed);

        if (transform.position.z < PlayerrTrasform.position.z - 20)
        {
            Destroy(gameObject);

        }

    }
}
