/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class WaterPart : MonoBehaviour
{
    #region Fields
    private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float resize_ratio;

    private bool is_time_to_move;
    private WaterZone waterZone_refference;
    private Rigidbody rb;
    private Vector3 localScale;
    private Collider this_collider;

    private UFO_Controller player_refference;
    [HideInInspector] public Renderer water_renderer;
    #endregion

    #region Unity Methods

    void Start()
    {
        water_renderer = GetComponent<Renderer>();
        player_refference = GameObject.FindGameObjectWithTag("Player").GetComponent<UFO_Controller>();
        this_collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        target = UFO_Controller.in_out_point_refference;
        waterZone_refference = transform.parent.GetComponent<WaterZone>();
        is_time_to_move = false;
        localScale = transform.localScale;
    }

    void FixedUpdate()
    {
        if (is_time_to_move)
        {
            MoveToTarget();
        }
    }
    #endregion

    public void MoveToTarget()
    {
        this_collider.isTrigger = true;
        target = UFO_Controller.in_out_point_refference;
        rb.isKinematic = true;

        if (Vector3.Distance(transform.position, target.position) >= 0.1)
        {
            is_time_to_move = true;
            transform.position += (target.position - transform.position) * Time.fixedDeltaTime * speed;

            //if (transform.localScale.x >= 0.005)
            //    transform.localScale -= localScale * Time.fixedDeltaTime * resize_ratio;
        }
        else
        {
            // At this Point Add this material to the Water asset
            //waterZone_refference.water_parts.Remove(this);
            gameObject.GetComponent<Resource>().enabled = true;
            player_refference.Water_Collection.Add(gameObject.GetComponent<Resource>());
            water_renderer.material = waterZone_refference.water_material;
            is_time_to_move = false;

            gameObject.SetActive(false);
        }
    }
}
