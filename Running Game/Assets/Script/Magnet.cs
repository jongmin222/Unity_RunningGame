using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private Vector3 upPosition;
    private Vector3 downPosition;
    private float degree = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.upPosition = this.transform.position;
        this.downPosition = this.transform.position;
        this.upPosition.y += 0.8f;
        this.downPosition.y -= 0.8f;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.Find("MagnetCollider").gameObject.activeSelf == false)
        {
            this.upPosition.x = this.transform.position.x;
            this.upPosition.z = this.transform.position.z;
            this.downPosition.x = this.transform.position.x;
            this.downPosition.z = this.transform.position.z;

            this.degree += Time.deltaTime * 3;

            this.transform.position = Vector3.Lerp(this.upPosition, this.downPosition,
                (Mathf.Sin(this.degree) + 1) * 0.5f);
        }
    }
}
