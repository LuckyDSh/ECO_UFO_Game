/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class WoodPart : MonoBehaviour
{
    #region Fields
    private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float resize_ratio;

    private bool is_time_to_move;
    private HouseZone houseZone_refference;
    private Rigidbody rb;
    private Vector3 localScale;
    private Collider this_collider;
    #endregion

    private void Start()
    {
        this_collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;
        target = UFO_Controller.in_out_point_refference;
        houseZone_refference = transform.parent.GetComponent<HouseZone>();
        is_time_to_move = false;
        localScale = transform.localScale;
    }

    public void FixedUpdate()
    {
        if (is_time_to_move)
        {
            MoveToTarget();
        }
    }

    public void MoveToTarget()
    {
        target = UFO_Controller.in_out_point_refference;
        rb.isKinematic = true;
        this_collider.isTrigger = true;

        if (Vector3.Distance(transform.position, target.position) >= 0.1)
        {
            is_time_to_move = true;
            transform.position += (target.position - transform.position) * Time.fixedDeltaTime * speed;

            if (transform.localScale.x >= 0.005)
                transform.localScale -= localScale * Time.fixedDeltaTime * resize_ratio;
        }
        else
        {
            // At this Point Add this material to the Tree asset
            //houseZone_refference.wood_parts.Remove(this);
            gameObject.SetActive(false);
        }
    }
}
