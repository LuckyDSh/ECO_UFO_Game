/*
*	TickLuck
*	All rights reserved
*/
using UnityEngine;

public class Follow : MonoBehaviour
{
    #region Fields
    [SerializeField] private Transform target;
    #endregion

    private void LateUpdate()
    {
        transform.position = target.position;
    }
}
