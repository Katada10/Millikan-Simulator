using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilDropper : MonoBehaviour {
    private ParticleSystem system;
    private List<ParticleCollisionEvent> events;

    public Transform position;
    public Transform oilPrefab;


	private bool oilDropped;

    void Start()
    {
        system = GetComponent<ParticleSystem>();
        events = new List<ParticleCollisionEvent>();
        oilDropped = false;
    }

	void OnParticleCollision(GameObject obj)
	{
		if(obj.name == "Oil Drop Trigger")
        {
            obj.GetComponent<Collider>().enabled = false;
            if(!oilDropped)
            {
                oilDropped = true;
                Instantiate(oilPrefab, position.transform.position,  Quaternion.identity);
            }
        }
	}
}
