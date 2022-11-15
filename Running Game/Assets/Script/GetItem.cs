using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    private AudioSource audioSource = null;
    public AudioClip Coin = null;
    public AudioClip Magnet = null;
    public AudioClip Acorn = null;
    public int getCoinNum = 0;

    private GameObject moveObject = null;
    private FlyBarScript flyBar = null;
    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
        this.flyBar = this.GetComponent<PlayerControl>().flyBar;
        this.audioSource.volume = 0.1f;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            this.audioSource.clip = Coin;
            this.audioSource.Play();

            this.getCoinNum++;
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Magnet")
        {
            this.audioSource.clip = Magnet;
            this.audioSource.Play();

            collision.gameObject.transform.Find("MagnetCollider").gameObject.SetActive(true);
            this.moveObject = collision.gameObject;
            Destroy(collision.gameObject,15);
        }

        if (collision.gameObject.tag == "Acorn")
        {
            this.audioSource.clip = Acorn;
            this.audioSource.Play();
            this.flyBar.getAcorn(500.0f);
            Destroy(collision.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.moveObject)
            this.moveObject.transform.position = this.transform.position + Vector3.up / 2;
    }
}
