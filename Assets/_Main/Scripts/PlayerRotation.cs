using Unity.Collections;
using UnityEngine;

/// <summary>
/// Player Rotation.
/// 
/// 'PlayerRotation' is in charge of rotating the character towards the cursor
/// This script will be a child of the player object
/// The angle of rotation should be taken in consideration to shoot but not to dash
/// </summary>
/// 
public class PlayerRotation : MonoBehaviour
{
    #region FIELDS

    private float _angle;

    #endregion

    #region PROPERTIES

    /// <summary>
    /// Actual angle for character Y rotation (in degrees).
    /// </summary>
    public float angle {get; private set;}

    #endregion

    #region METHODS

    /// <summary>
    /// 'RotationInput' makes the object Z axis to face the cursor
    /// It checks the object location and the cursor location in the camera
    /// From those two points it gets the angle between them
    /// </summary>
    /// 
    private void RotationInput()
    {
        Vector2 objectpositionOnScreen = Camera.main.WorldToViewportPoint(transform.position);
        Vector2 cursorpositionOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);
        float angle = AngleBetweenTwoPoints(objectpositionOnScreen, cursorpositionOnScreen);
        _angle = FixAngleToCharacterY(angle);
    }

    private void RotateTo(float angle)
    {
        /// <summary>
        /// 'Rotate' applies the rotation y to the transform while maintaining x and z in zero
        /// </summary>
        /// 
        transform.rotation = Quaternion.Euler(new Vector3(default, angle, default));
    }


    private float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        /// <summary>
        /// 'AngleBetweenTwoPoints' returns the arctangent between two points
        /// The order will vary the result
        /// It returns the angle from an invisible line to the right from point b
        /// to the line generated between the points
        /// </summary>
        /// 
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }

    private float FixAngleToCharacterY(float angle)
    {
        /// <summary>
        /// 'FixAngleToCharacterY' Corrects the angle obtained with the arctangent
        /// It maps the angle to be rotated 90 degrees and inverted
        /// The result will make the character forward to be at 0 degrees
        /// </summary>
        /// 
        // Angles need to be corrected from usual angles to angles of the character (Only moving Y)
        angle += 90;
        angle *= -1;
        return angle;
    }

    #endregion

    #region MONOBEHAVIOUR

    private void FixedUpdate()
    {
        RotateTo(_angle);
    }

    private void Update()
    {
        RotationInput();
    }

    #endregion
}
