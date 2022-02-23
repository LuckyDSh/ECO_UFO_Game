/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class Resource : MonoBehaviour
{
    #region Fields
    [SerializeField] private float speed;
    [SerializeField] private float resize_ratio;

    public Vector3 original_size;
    public Vector3 small_size;

    private Transform target;
    private bool is_time_to_move;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Collider this_collider;
    #endregion

    #region Unity Methods
    void Start()
    {
        transform.localScale = small_size;

        if (gameObject.tag == "Water")
        {
            rb = GetComponent<Rigidbody>();
            this_collider = GetComponent<Collider>();
        }
    }

    void FixedUpdate()
    {
        if (is_time_to_move)
        {
            MoveToTarget(target);
        }
    }
    #endregion

    public void MoveToTarget(Transform resource_target)
    {
        target = resource_target;

        if (Vector3.Distance(transform.position, target.position) >= 0.1)
        {
            is_time_to_move = true;
            transform.position += (target.position - transform.position) * Time.fixedDeltaTime * speed;

            if (transform.localScale.x <= original_size.x)
                transform.localScale += original_size * Time.fixedDeltaTime * resize_ratio;
        }
        else
        {
            is_time_to_move = false;
            transform.position = target.position;
        }
    }
}
