using UnityEngine;

public class ObjectSizeCalculator
{
    public static Vector2 CalculateObjectSizeOnCamera(GameObject obj, Camera cam)
    {
        Bounds objBounds = obj.GetComponent<Collider>().bounds;
        Vector3 boundsCenter = objBounds.center;
        Vector3 boundsExtents = objBounds.extents;

        //all bounds points
        Vector3[] boundsVertices = new Vector3[8]
        {
            boundsCenter + new Vector3(-boundsExtents.x, -boundsExtents.y, -boundsExtents.z),
            boundsCenter + new Vector3(-boundsExtents.x, -boundsExtents.y, boundsExtents.z),
            boundsCenter + new Vector3(boundsExtents.x, -boundsExtents.y, -boundsExtents.z),
            boundsCenter + new Vector3(boundsExtents.x, -boundsExtents.y, boundsExtents.z),
            boundsCenter + new Vector3(-boundsExtents.x, boundsExtents.y, -boundsExtents.z),
            boundsCenter + new Vector3(-boundsExtents.x, boundsExtents.y, boundsExtents.z),
            boundsCenter + new Vector3(boundsExtents.x, boundsExtents.y, -boundsExtents.z),
            boundsCenter + new Vector3(boundsExtents.x, boundsExtents.y, boundsExtents.z)
        };

        Vector2[] boundsVerticesOnScreen = new Vector2[8];

        for (int i = 0; i < 8; i++)
        {
            boundsVerticesOnScreen[i] = cam.WorldToScreenPoint(boundsVertices[i]);
        }

        Vector2 middlePoint = new Vector2(Screen.width / 2, Screen.height / 2);

        //calculate most left point
        Vector2 mostLeftPoint = middlePoint;
        foreach (Vector2 point in boundsVerticesOnScreen)
        {
            if (point.x < mostLeftPoint.x) mostLeftPoint = point;
        }
        //calculate most right point
        Vector2 mostRightPoint = middlePoint;
        foreach (Vector2 point in boundsVerticesOnScreen)
        {
            if (point.x > mostRightPoint.x) mostRightPoint = point;
        }

        //calculate most up point
        Vector2 mostUpPoint = middlePoint;
        foreach (Vector2 point in boundsVerticesOnScreen)
        {
            if (point.y > mostUpPoint.y) mostUpPoint = point;
        }

        //calculate most down point
        Vector2 mostDownPoint = middlePoint;
        foreach (Vector2 point in boundsVerticesOnScreen)
        {
            if (point.y < mostDownPoint.y) mostDownPoint = point;
        }

        float objWidth = mostRightPoint.x - mostLeftPoint.x;
        float objHeight = mostUpPoint.y - mostDownPoint.y;

        return new Vector2(objWidth, objHeight);
    }
}
