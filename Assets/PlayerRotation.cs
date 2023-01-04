using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private float _angle;

    private void RotationInput()
    {

        // Get the screen position
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        // Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        // Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        // Convert the angle for the character
        _angle = FixAngleToCharacterY(angle);

        //Debug.Log("Screen: " + positionOnScreen + " Mouse: " + mouseOnScreen + " Angle: " + _angle);
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0f, _angle, 0f));
    }


    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private float FixAngleToCharacterY(float angle)
    {
        // Angles need to be corrected from usual angles to angles of the character (Only moving Y)
        angle += 90;
        angle *= -1;
        return angle;
    }

    private void FixedUpdate()
    {
        Rotate();
    }

    private void Update()
    {
        RotationInput();
    }
}
