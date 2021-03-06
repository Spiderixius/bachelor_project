﻿using UnityEngine;
using System.Collections;

/// <summary>
/// The camera script which follows the player.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    // Focus area related
    public GameObject target;
    public Vector2 focusAreaSize;
    public float verticalOffSet;
    public float lookAheadDistanceX;
    public float lookSmoothTimeX;
    public float verticalSmoothTime;
    FocusArea focusArea;

    // Offset related
    float currentLookAheadX;
    float targetLookAheadX;
    float lookAheadDirectionX;
    float smoothLookVelocityX;
    float smoothVelocityY;

    // Camera bounds
    public bool bounds;
    public Vector3 minCameraPosition;
    public Vector3 maxCameraPostion;

    void Start()
    {

        focusArea = new FocusArea(target.GetComponent<Collider2D>().bounds, focusAreaSize);
    }

    void LateUpdate()
    {
        if (target != null)
        {
            focusArea.Update(target.GetComponent<Collider2D>().bounds);
            Vector2 focusPosition = focusArea.centre + Vector2.up * verticalOffSet;

            if (focusArea.velocity.x != 0)
            {
                lookAheadDirectionX = Mathf.Sign(focusArea.velocity.x);
            }

            targetLookAheadX = lookAheadDirectionX * lookAheadDistanceX;
            currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref smoothLookVelocityX, lookSmoothTimeX);

            focusPosition += Vector2.right * currentLookAheadX;
            transform.position = (Vector3)focusPosition + Vector3.forward * -10;

            if (bounds)
            {
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPosition.x, maxCameraPostion.x),
                    Mathf.Clamp(transform.position.y, minCameraPosition.y, maxCameraPostion.y),
                    Mathf.Clamp(transform.position.z, minCameraPosition.z, maxCameraPostion.z));
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, .5f);
        Gizmos.DrawCube(focusArea.centre, focusAreaSize);
    }

    /// <summary>
    /// Drawing of the focus area. 
    /// </summary>
    struct FocusArea
    {
        public Vector2 centre;
        public Vector2 velocity;
        float left, right;
        float top, bottom;

        public FocusArea(Bounds targetBounds, Vector2 size)
        {
            left = targetBounds.center.x - size.x / 2;
            right = targetBounds.center.x + size.x / 2;
            bottom = targetBounds.min.y;
            top = targetBounds.min.y + size.y;

            velocity = Vector2.zero;
            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
        }

        public void Update(Bounds targetBounds)
        {
            // Sides
            float shiftX = 0;
            if (targetBounds.min.x < left)
            {
                shiftX = targetBounds.min.x - left;
            }
            else if (targetBounds.max.x > right)
            {
                shiftX = targetBounds.max.x - right;
            }
            left += shiftX;
            right += shiftX;

            // bottom and top
            float shiftY = 0;
            if (targetBounds.min.y < bottom)
            {
                shiftY = targetBounds.min.y - bottom;
            }
            else if (targetBounds.max.y > top)
            {
                shiftY = targetBounds.max.y - top;
            }
            top += shiftY;
            bottom += shiftY;

            centre = new Vector2((left + right) / 2, (top + bottom) / 2);
            velocity = new Vector2(shiftX, shiftY);
        }
    }


}
