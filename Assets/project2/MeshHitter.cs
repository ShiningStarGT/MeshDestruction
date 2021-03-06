using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshHitter : MonoBehaviour
{
    private int destructableLayer = 1 << 3;
    public float searchRadius = 3f;
    public float force = 0.1f;
    public GameObject tstprefab;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetButtonDown("Fire1")) return;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, destructableLayer))
        {
            DestructableObject destructableObject = hit.collider.GetComponent<DestructableObject>();
            if (destructableObject)
            {
                int i = destructableObject.getNearbyVerticeIndex(hit.point, searchRadius, hit.triangleIndex);
                Debug.Log("closest index to our hit point is "+ i);

                if (i == -1) return;
                destructableObject.ChangeSimilarVertices(i,transform.position, force);
            }
            else
            {
                Debug.LogError("The object " + hit + " needs to have a DestructableObject script attached to it");
            }
        }
        Debug.DrawRay(transform.position, transform.forward * 8, Color.red, 2f);
    }
}