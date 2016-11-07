using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAverageLight : MonoBehaviour
{
    public Light averageLight;
    public Transform drone;

    public Transform fireL;
    public Transform fireR;

    float speed = 30f;


    void Update()
    {
        float i = 0;

//        Vector3.Lerp();

        Vector3 pos = drone.position;

        i += fireL.localScale.x / 2;
        i += fireR.localScale.x / 2;
        pos = Vector3.Lerp(fireL.position, fireR.position, -fireL.localScale.x / 2 + fireL.localScale.x / 2 + 0.5f);

        if (averageLight.intensity < i)
        {
            averageLight.intensity += Time.deltaTime * speed;
            if (averageLight.intensity > i)
                averageLight.intensity = i;
        } else if (averageLight.intensity > i)
        {
            averageLight.intensity -= Time.deltaTime * speed;
            if (averageLight.intensity < i)
                averageLight.intensity = i;
        }

        averageLight.transform.position = pos;


//        Debug.Log("drone.pos: " + drone.position);

    }

}
