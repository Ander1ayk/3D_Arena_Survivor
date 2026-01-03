using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform targetPlayer;
    public Vector3 offsetPos;
    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if(targetPlayer == null)
        {
            if(playerObj != null)
            {
                targetPlayer = playerObj.transform;
            }
        }
    }
    private void LateUpdate()
    {
        FollowTarget();
    }
    private void FollowTarget()
    {
        transform.position = targetPlayer.position + offsetPos;
    }
}
