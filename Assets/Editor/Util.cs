using UnityEngine;

public static class Util {
    public static Vector4 FloatToVector4(float f, float w) => new(f, f, f, w);

    public static Vector4 Vector3ToVector4(Vector3 v, float w) => new(v.x, v.y, v.z, w);
}