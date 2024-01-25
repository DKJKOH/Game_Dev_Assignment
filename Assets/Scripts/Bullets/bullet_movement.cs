using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_movement : MonoBehaviour
{

	[SerializeField]
	float force = 1000;


    public BulletTrailScriptableObject TrailConfig;
    protected TrailRenderer Trail;
    protected Transform Target;

    [SerializeField]
    private Renderer Renderer;

    private bool IsDisabling = false;

    protected const string DISABLE_METHOD_NAME = "Disable";
    // Helps with not destroying the bullet trail after the trail has caught up with the bullet (Delay)
    protected const string DO_DISABLE_METHOD_NAME = "DoDisable";

    public Rigidbody2D Rigidbody;


    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        Trail = GetComponent<TrailRenderer>();
    }

    protected virtual void OnEnable()
    {
        Renderer.enabled = true;
        IsDisabling = false;

        // Call function called configure trail
        ConfigureTrail();

    }

    private void ConfigureTrail()
    {
        if (Trail != null && TrailConfig != null)
        {
            // Call the Trail Setup function within the Trailconfig script
            TrailConfig.TrailSetup(Trail);
        }
    }

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
    	// calling rigidbody2d
        rb = GetComponent<Rigidbody2D>();

        // Adding bullet force (In Y direction as it is 2D) - Force is impulse as it is an explosion.
        rb.AddForce(transform.up * force * Time.deltaTime, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
    	
    }

    protected void Disable()
    {
        CancelInvoke(DISABLE_METHOD_NAME);
        CancelInvoke(DO_DISABLE_METHOD_NAME);

        Renderer.enabled = false;

        // If there is a trail
        if (Trail != null && TrailConfig != null)
        {
            IsDisabling= true;
            // Execute this after a period of time
            Invoke(DO_DISABLE_METHOD_NAME, TrailConfig.Time);
        }
        else
        {
            DoDisable();
        }
        // Enable bullet game object
        gameObject.SetActive(false);

    }

    protected void DoDisable()
    {
        // Clear trail before disabling object
        if (Trail != null && TrailConfig != null)
        {
            Trail.Clear();
        }

        // Disable bullet
        gameObject.SetActive(false);
    }
}
