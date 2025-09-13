using UnityEngine;

public class ScreenWrap : MonoBehaviour
{

    public float edgeDistance;

    private Camera cam;
    private float halfWidth;
    private float halfHeight;

    private GameObject[] clones = new GameObject[3];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;

    }

    void Update()
    {
        Vector3 pos = transform.position;

        Vector3 xOffset = new Vector3(2f * halfWidth, 0, 0);
        Vector3 yOffset = new Vector3(0, 2f * halfHeight, 0);

        bool nearX = Mathf.Abs(pos.x) > halfWidth - edgeDistance;
        bool nearY = Mathf.Abs(pos.y) > halfHeight - edgeDistance;

        Vector3 xValue = (pos.x > 0 ? -xOffset : xOffset);
        Vector3 yValue = (pos.y > 0 ? -yOffset : yOffset);

        DrawClone(0, nearX, pos + xValue);
        DrawClone(1, nearY, pos + yValue);
        DrawClone(2, nearX && nearY, pos + xValue + yValue);

        Vector3 originalPos = pos;

        bool wrappedX = false;
        bool wrappedY = false;

        if (pos.x > halfWidth) { pos -= xOffset; wrappedX = true; }
        if (pos.x < -halfWidth) { pos += xOffset; wrappedX = true; }
        if (pos.y > halfHeight) { pos -= yOffset; wrappedY = true; }
        if (pos.y < -halfHeight) { pos += yOffset; wrappedY = true; }

        transform.position = pos;

        if (wrappedX)
        {
            if (clones[0] != null) clones[0].transform.position = originalPos;
            if (clones[2] != null) clones[2].transform.position = originalPos;
        }
        if (wrappedY)
        {
            if (clones[1] != null) clones[1].transform.position = originalPos;
            if (clones[2] != null) clones[2].transform.position = originalPos;
        }
    }

    void DrawClone(int index, bool shouldExist, Vector3 targetPos)
    {
        if (shouldExist)
        {
            if (clones[index] == null)
            {
                clones[index] = Instantiate(gameObject, targetPos, transform.rotation);
                Destroy(clones[index].GetComponent<ScreenWrap>());

                clones[index].AddComponent<CloneMarker>();
            }
            else
            {
                clones[index].transform.position = targetPos;
                clones[index].transform.rotation = transform.rotation;
            }

        }
        else if (clones[index] != null)
        {
            Destroy(clones[index]);
            clones[index] = null;

        }

    }

    void OnDestroy()
    {
        foreach (GameObject clone in clones)
        {
            if (clone != null)
            {
                Destroy(clone);
            }
        }
    }
}
