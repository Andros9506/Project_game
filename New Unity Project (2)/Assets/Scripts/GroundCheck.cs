using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask platformLayerMask;
    public bool isGround = true;
    private void OnTriggerStay2D(Collider2D collider)
    {
        isGround = collider != null && (((1 << collider.gameObject.layer) & platformLayerMask) != 0);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        isGround = false;
    }
    private void Log()
    {
        Debug.Log(isGround);
    }
}
