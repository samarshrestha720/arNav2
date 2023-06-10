using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SetNavigationTarget : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private Camera topDownCamera;
    [SerializeField]
    private GameObject navTargetObject;
    private NavMeshPath path;//current calculated path
    private LineRenderer line; //linereanderer to display path
    private bool lineToggle = true; //start and stop calculation
    void Start()
    {
        path = new NavMeshPath(); //craeting new path
        line = transform.GetComponent<LineRenderer>(); //getting line rendering component every time we touch the screen
    }
    // Update is called once per frame
    void Update()
    {
        if ((Input.touchCount > 0) && (Input.GetTouch(0).phase == TouchPhase.Began))
        {
            lineToggle = !lineToggle;
        }
        if (lineToggle)
        {
            NavMesh.CalculatePath(transform.position, navTargetObject.transform.position, NavMesh.AllAreas, path);
            line.positionCount = path.corners.Length;
            line.SetPositions(path.corners);
            line.enabled = true;
        }
    }
}
