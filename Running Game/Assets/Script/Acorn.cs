using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acorn : MonoBehaviour
{
    private Vector3 upPosition;
    private Vector3 downPosition;
    private float degree = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.upPosition = this.transform.position;
        this.downPosition = this.transform.position;
        this.upPosition.y += 0.2f;
        this.downPosition.y -= 0.2f;
    }
    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            this.transform.position += new Vector3(0, 0.2f, 0);
            this.upPosition = this.transform.position;
            this.downPosition = this.transform.position;
            this.upPosition.y += 0.2f;
            this.downPosition.y -= 0.2f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        this.degree += Time.deltaTime * 2;

        this.transform.position = Vector3.Lerp(this.upPosition, this.downPosition,
    (Mathf.Sin(this.degree) + 1) * 0.5f);
    }
}
