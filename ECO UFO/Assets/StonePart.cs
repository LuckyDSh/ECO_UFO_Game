/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class StonePart : MonoBehaviour
{
    #region Fields
    private Transform target;
    [SerializeField] private float speed;
    [SerializeField] private float resize_ratio;

    private bool is_time_to_move;
    private StoneZone stoneZone_refference;
    private Rigidbody rb;
    private Vector3 localScale;
    private Collider this_collider;
    #endregion

    private void Start()
    {
        this_collider = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        target = UFO_Controller.in_out_point_refference;
        stoneZone_refference = transform.parent.GetComponent<StoneZone>();
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
