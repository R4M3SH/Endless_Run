using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sPawnManager : MonoBehaviour
{

    public GameObject[] vehicalPrefab;
    public Transform Road;
    public int vehicleInt;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position == new Vector3(0, 0, 3))
        {
            Instantiate(vehicalPrefab[vehicleInt], Road.position, vehicalPrefab[vehicleInt].transform.rotation);
        }
    }
}
