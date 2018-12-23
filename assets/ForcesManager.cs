using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForcesManager : MonoBehaviour {
    private GameObject oilDrop;
    private Rigidbody rb;

    public float particleSize;
    public GameObject topPlate;
    public GameObject bottomPlate;
    public Slider voltage;
    public Toggle toggle;
    public Text voltageLabel;
    public Button changeNumElectrons;

    public Text forceLabel, chargeLabel;


    private float numOfElectrons;
    private readonly float density = 930f;

    void Start()
    {
        changeNumElectrons.onClick.AddListener(OnChange);
        numOfElectrons = Random.Range(1, 10);
    }

    void OnChange()
    {
        numOfElectrons = Random.Range(1, 10);
    }

    private void CalculateForces(Rigidbody rb)
    {
        Time.timeScale = 0.2f;

        var radius = oilDrop.GetComponent<SphereCollider>().radius / 1000;
        var mass = density * 4 * Mathf.PI * Mathf.Pow(radius, 3);
        rb.mass = particleSize;
        var factor = rb.mass / mass;

        var gravity = 0f;

        var distance = Mathf.Abs(bottomPlate.transform.position.y - topPlate.transform.position.y);
        
        var charge = (particleSize * numOfElectrons) * (1.6f * Mathf.Pow(10,-8)) * factor;
        var factoredCharge = charge / factor / Mathf.Pow(10, 11);

        var force = 0f;

        if (!toggle.isOn)
        {
            force = ((voltage.value * charge) / distance) / 3;
            forceLabel.text = "FE: " + ((voltage.value * factoredCharge) / (distance / 100));
            gravity = (rb.mass * -10f) / 3;
            rb.isKinematic = false;
        }
        else
        {
            voltage.value = (10f * distance) / charge;
            force = (voltage.value * charge) / distance;
            forceLabel.text = "FE: " + (voltage.value * factoredCharge) / (distance / 100);
            gravity = -10f;
            rb.isKinematic = true;
        }

        chargeLabel.text = "Distance: " + (distance / 100) + "m";
        rb.AddForce(new Vector3(0, force + gravity, 0));
    }

    void FixedUpdate()
    {
        voltageLabel.text = "Voltage: " + Mathf.Round(voltage.value * 100) / 100 + "V";
        oilDrop = GameObject.FindGameObjectWithTag("Oil");
        if (oilDrop != null)
        {
            rb = oilDrop.GetComponent<Rigidbody>();
            CalculateForces(rb);
        }
    }
}
