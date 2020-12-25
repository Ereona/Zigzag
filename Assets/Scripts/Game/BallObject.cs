using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallObject : MonoBehaviour
{
    public int index;
    public Rigidbody Rb;

    private void OnCollisionEnter(Collision collision)
    {
        BlockObject block = collision.collider.GetComponent<BlockObject>();
        if (block != null)
        {
            index = block.index;
            //avoid bouncing
            if (Rb.velocity.y > 0)
            {
                Rb.velocity =
                    new Vector3(Rb.velocity.x, 0, Rb.velocity.z);
                this.transform.position = new Vector3(this.transform.position.x, this.transform.localScale.y / 2, this.transform.position.z);
            }
        }
    }
}
