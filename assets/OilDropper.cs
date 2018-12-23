using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OilDropper : MonoBehaviour {
    private ParticleSystem system;
    private List<ParticleCollisionEvent> events;

    public Transform position;
    public Transform oilPrefab;
 
    public Button button;

	private bool oilDropped;

    void Start()
    {
        system = GetComponent<ParticleSystem>();
        events = new List<ParticleCollisionEvent>();
        oilDropped = false;
        button.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        system.Play();
    }

	void OnParticleCollision(GameObject obj)
	{
		if(obj.name == "Oil Drop Trigger")
        {
            obj.GetComponent<Collider>().enabled = false;
            if(!oilDropped)
            {
                oilDropped = true;
                Instantiate(oilPrefab, position.transform.position, Quaternion.identity);
            }
        }
	}
}
