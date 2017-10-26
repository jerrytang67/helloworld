namespace ESP.Model
{
    public class APlayerCameraManager
    {
        public FCameraCacheEntry CameraCache { get; set; }
    }

    public class FCameraCacheEntry
    {
        public FMinimalViewInfo POV { get; set; }
    }

    public class FMinimalViewInfo
    {
        public Vector3 Location { get; set; }          
        public FRotator Rotation  { get; set; }
        public float Fov { get; set; }
    }

}