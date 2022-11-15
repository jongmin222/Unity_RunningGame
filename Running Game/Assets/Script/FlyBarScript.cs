using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FlyBarScript : MonoBehaviour
{
    public Slider flyBar = null;
    private float maxGage;
    public float currentGage;

    void Start()
    {
        this.maxGage = 1000;
        this.currentGage = 1000;
    }

    public void getAcorn(float amount)
    {
        this.currentGage += amount;
        return;
    }

    void Update()
    {
        this.currentGage = Mathf.Clamp(this.currentGage, 0, 1000);
        this.flyBar.value = this.currentGage / this.maxGage;
    }
}
