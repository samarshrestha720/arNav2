using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class SetNavigationTarget : MonoBehaviour
{
    [SerializeField]
    private TMP_Dropdown navigationTargetDropDown;
    [SerializeField]
    private List<Target> navigationTargetObjects = new List<Target>();

    private NavMeshPath path; // current calculated path
    private LineRenderer line; // linerenderer to display path
    private Vector3 targetPosition = Vector3.zero; // current target position

    public int currentFloor = 2;
    private bool lineToggle = false;

    private void Start()
    {
        path = new NavMeshPath();
        line = transform.GetComponent<LineRenderer>();
        line.enabled = lineToggle;
    }

    private void Update()
    {
        if (lineToggle && targetPosition != Vector3.zero)
        {
            NavMesh.CalculatePath(transform.position, targetPosition, NavMesh.AllAreas, path);
            line.positionCount = path.corners.Length;
            line.SetPositions(path.corners);

        }
    }


    public void SetCurrentNavigationTarget(int selectedValue)
    {
        targetPosition = Vector3.zero;
        string selectedText = navigationTargetDropDown.options[selectedValue].text;
        Target currentTarget = navigationTargetObjects.Find(x =>
        {
            return x.Name.ToLower().Equals(selectedText.ToLower());
        });
        if (currentTarget != null)
        {
            if (!line.enabled)
            {
                ToggleVisibility();
            }

            // check if floor is changing
            // if yes, lead to elevator
            // if no, navigate
            targetPosition = currentTarget.PositionObject.transform.position;
        }
    }
    public void ToggleVisibility()
    {
        lineToggle = !lineToggle;
        line.enabled = lineToggle;
    }
    public void ChangeActiveFloor(int floorNumber)
    {
        currentFloor = floorNumber;
        SetNavigationTargetDropDownOptions(currentFloor);
    }


    private void SetNavigationTargetDropDownOptions(int floorNumber)
    {
        navigationTargetDropDown.ClearOptions();
        navigationTargetDropDown.value = 0;

        if (line.enabled)
        {
            ToggleVisibility();
        }

        if (floorNumber == 1)
        {
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("L101"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("L102"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("L103"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("HOD"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Cabin"));
        }
        else if (floorNumber == 2)
        {
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("ProjectRoom"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("L201"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("L202"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("L203"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("L204"));
        }
        else if (floorNumber == 3)
        {
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("TeachersCabin"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("ExamDiv"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("ProjectDiv"));
        }
        else if (floorNumber == 0)
        {
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Library"));
            navigationTargetDropDown.options.Add(new TMP_Dropdown.OptionData("Upstairs"));
        }
    }

}