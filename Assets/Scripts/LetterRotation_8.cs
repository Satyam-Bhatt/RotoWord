using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterRotation_8 : MonoBehaviour
{
    [SerializeField] private Location location;
    [SerializeField] private Transform parent;

    private float angle_Complex;

    // Update is called once per frame
    void Update()
    {
        int angle = Mathf.RoundToInt(parent.transform.rotation.eulerAngles.z);

        if(location == Location.FirstQuadrant_1Letter)
        {
            if (angle > 45 && angle <= 90)
            {
                transform.rotation = Quaternion.Euler(0, 0, RotationCalculator(parent.transform.rotation.eulerAngles.z, 45, 75));
            }
            else if (angle - 360 > -135 && angle - 360 <= -90)
            {
                transform.rotation = Quaternion.Euler(0, 0, RotationCalculator(parent.transform.rotation.eulerAngles.z, 225, -285));
            }
        }
        else if(location == Location.FirstQuadrant_2Letter)
        {
            if (angle > 0 && angle <= 45)
            {
                transform.rotation = Quaternion.Euler(0, 0, RotationCalculator(parent.transform.rotation.eulerAngles.z, 0, 60) + 15 * angle_Complex);
            }
            else if (angle - 360 > -180 && angle - 360 <= -135)
            {
                transform.rotation = Quaternion.Euler(0, 0, RotationCalculator(parent.transform.rotation.eulerAngles.z, 180, -285));
            }
        }
        else if (location == Location.SecondQuadrant_1Letter)
        {
            if (angle > 135 && angle <= 180)
            {
                transform.rotation = Quaternion.Euler(0, 0, RotationCalculator(parent.transform.rotation.eulerAngles.z, 135, 60) + 15 * angle_Complex);
            }
            else if (angle - 360 > -45 && angle - 360 <= 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, RotationCalculator(parent.transform.rotation.eulerAngles.z, 315, -285));
            }
        }

        else if (location == Location.SecondQuadrant_2Letter)
        {
            if (angle > 90 && angle <= 135)
            {
                transform.rotation = Quaternion.Euler(0, 0, RotationCalculator(parent.transform.rotation.eulerAngles.z, 90, 75));
            }
            else if (angle - 360 > -90 && angle - 360 <= -45)
            {
                transform.rotation = Quaternion.Euler(0, 0, RotationCalculator(parent.transform.rotation.eulerAngles.z, 270, -285));
            }
        }
    }

    private float RotationCalculator(float currentAngle, float quadrantInitialAngle, float letterAngle)
    {
        angle_Complex = (currentAngle - quadrantInitialAngle) / 45;
        return letterAngle - 140 * angle_Complex;
    }

    private enum Location
    {
        FirstQuadrant_1Letter,
        FirstQuadrant_2Letter,
        SecondQuadrant_1Letter,
        SecondQuadrant_2Letter
    }
}
