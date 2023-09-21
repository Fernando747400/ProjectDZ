using UnityEngine;

[System.Serializable]
public class bodySocket
{
    public GameObject gameObject;
    [Range(0f, 1f)]
    public float heighRatio;
}

public class BodySocketInventory : MonoBehaviour
{
    public GameObject HMD;
    public bodySocket[] bodySockets;

    private Vector3 _currentHMDPosition;
    private Quaternion _currentHMDRotation;

    private void Update()
    {
        _currentHMDPosition = HMD.transform.position;
        _currentHMDRotation = HMD.transform.rotation;
        foreach(var bodySocket in bodySockets)
        {
            updateBodySocketHeight(bodySocket);
        }
        UpdateSocketInventory();
    }

    private void updateBodySocketHeight(bodySocket bodySocket)
    {
        bodySocket.gameObject.transform.position = new Vector3(bodySocket.gameObject.transform.position.x, _currentHMDPosition.y * bodySocket.heighRatio, bodySocket.gameObject.transform.position.z);
    }

    private void UpdateSocketInventory()
    {
        transform.position = new Vector3(_currentHMDPosition.x, 0, _currentHMDPosition.z);
        transform.rotation = new Quaternion(transform.rotation.x, _currentHMDRotation.y, transform.rotation.z, _currentHMDRotation.w);
    }
}
