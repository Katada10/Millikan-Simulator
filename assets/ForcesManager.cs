using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcesManager : MonoBehaviour {
    private float radiusTop;
    private GameObject oilDrop;
    private Rigidbody rb;

    public float particleSize;
    public GameObject topPlate;
    public GameObject bottomPlate;

    //Kg/m^3
    private readonly float density = 930f;
    private readonly float voltage = 700;

    private void CalculateForces(Rigidbody rb)
    {
        var radius = oilDrop.GetComponent<SphereCollider>().radius / 1000;
        var mass = density * 4 * Mathf.PI * Mathf.Pow(radius, 3);
        rb.mass = particleSize;
        var factor = rb.mass / mass;

        var gravity = rb.mass * -10f;
        rb.AddForce(new Vector3(0, gravity, 0));

        var distance = Mathf.Abs(bottomPlate.transform.position.y - topPlate.transform.position.y);

        //DONT DELETE
        var charge = particleSize * (1.6f * Mathf.Pow(10,-9)) * factor;
        var factoredCharge = charge / factor / Mathf.Pow(10, 10);

        rb.AddForce(new Vector3(0, (voltage * charge) / distance, 0));

        Debug.Log(factoredCharge);
    }

    void FixedUpdate()
    {
        oilDrop = GameObject.FindGameObjectWithTag("Oil");
        if (oilDrop != null)
        {
            radiusTop = oilDrop.transform.position.y - topPlate.transform.position.y;

            rb = oilDrop.GetComponent<Rigidbody>();
            CalculateForces(rb);
        }
    }
}
