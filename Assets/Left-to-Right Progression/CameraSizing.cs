using UnityEngine;
using System.Collections;

public static class CameraSizing {
    public static Bounds CameraBounds(this Camera myCamera)
    {
        float screenRatio = Screen.width / Screen.height;
        float cameraHeight = myCamera.orthographicSize * 2;
        Bounds bounds = new Bounds(myCamera.transform.position, new Vector3(cameraHeight * screenRatio, cameraHeight, -10));
        return bounds;
    }
}
