using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera cam;

    public int fontSize = 40;
    private string hitSpot;
    public int hitX = 10;
    public int hitY = 10;

    void Start()
    {
        cam = GetComponent<Camera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnGUI()
    {
        int size = 12;

        GUIStyle headStyle = new GUIStyle();
        headStyle.fontSize = fontSize;

        float posX = cam.pixelWidth/2 - size/4;
        float posY = cam.pixelHeight/2 - size/2;
        GUI.Label(new Rect(posX, posY, size, size), "*", headStyle);

        GUI.Label(new Rect(hitX, hitY, size, size), hitSpot, headStyle);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);
            Ray ray = cam.ScreenPointToRay(point);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();

                if (target != null)
                {
                    target.ReactToHit();
                    Debug.Log("Target hit");
                }
                else
                {
                    StartCoroutine(SphereIndicator(hit.point));
                    //Debug.Log("Hit " + hit.point);
                    hitSpot = Convert.ToString(hit.point);
                }
            }
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;

        yield return new WaitForSeconds(1);

        Destroy(sphere);
    }
}
