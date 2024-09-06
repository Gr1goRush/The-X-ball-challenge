using UnityEngine;

public static class Utility
{

    public static Vector3 RandomVector3(Vector3 min, Vector3 max)
    {
        Vector3 result = new Vector3();
        result.x = Random.Range(min.x, max.x);
        result.y = Random.Range(min.y, max.y);
        result.z = Random.Range(min.z, max.z);

        return result;
    }
}