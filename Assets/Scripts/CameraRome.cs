using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class CameraRome : MonoBehaviour
{
    public GameObject target;
    public float rotateSpeed = 10.0f;
    private Vector3 point;

    void Start()
    {
        point = target.transform.position;
        transform.LookAt(point);
    }

    private void Update()
    {
        transform.RotateAround(point, new Vector3(0.0f, 1.0f, 0.0f), 20 * Time.deltaTime * rotateSpeed);
    }

}