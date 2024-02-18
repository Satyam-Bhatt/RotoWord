using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterRotation : MonoBehaviour
{
    [SerializeField] private bool isPresent_1st_3rd_Quadrant = false;
    [SerializeField] private Transform parent;

    // Update is called once per frame
    void Update()
    {
        int angle = Mathf.RoundToInt(parent.transform.rotation.eulerAngles.z);
        
        if (isPresent_1st_3rd_Quadrant)  //for 1st and 3rd Quadrant
        {
            if (angle > 0 && angle <= 90)
            {
                transform.rotation = Quaternion.Euler(0, 0, RotationCalculator(parent.transform.rotation.eulerAngles.z, 0, 45));
            }
            else if (angle - 360 > -180 && angle - 360 <= -90)
            {
                transform.rotation = Quaternion.Euler(0, 0, RotationCalculator(parent.transform.rotation.eulerAngles.z, 180, -315));
            }
        }
        else //for 2nd and 4th Quadrant
        {
            if (angle > 90 && angle <= 180)
            {
                transform.rotation = Quaternion.Euler(0, 0, RotationCalculator(parent.transform.rotation.eulerAngles.z, 90, -315));
            }
            else if (angle - 360 > -90 && angle - 360 <= 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, RotationCalculator(parent.transform.rotation.eulerAngles.z, 270, 45));
            }
        }
    }

    private float RotationCalculator(float currentAngle,float quadrantInitialAngle, float letterAngle)
    {
        float angle_Complex = (currentAngle - quadrantInitialAngle) / 90;
        return letterAngle - 90 * angle_Complex;
    }
}
