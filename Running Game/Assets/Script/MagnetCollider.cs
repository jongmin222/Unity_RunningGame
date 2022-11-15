using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCollider : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            collision.gameObject.transform.position =
                Vector3.Lerp(collision.gameObject.transform.position, this.transform.position,
                Time.deltaTime * 10 / Vector3.Distance(collision.gameObject.transform.position, this.transform.position));

            Vector3 temp;
            temp.x = this.transform.position.x;
            temp.y = collision.gameObject.transform.position.y;
            temp.z = collision.gameObject.transform.position.z;

            if (collision.gameObject.transform.position.x < this.transform.position.x)
            {
                collision.gameObject.transform.position = temp;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
