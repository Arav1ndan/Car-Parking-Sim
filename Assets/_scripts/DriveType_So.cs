using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DriveType",menuName = "Custom/DriveType")]
public class DriveType_So : ScriptableObject
{
  
    public enum DriveType { FrontWheel, RearWheel, AllWheel }
    public DriveType driveType;

    public float maxAccelerationTorque = 500f;
    public float maxBrakeTorque = 10000f;

}
