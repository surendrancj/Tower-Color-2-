using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    [SerializeField] CameraController cameraController;
    [SerializeField] Camera mainCam;
    [SerializeField] BlockCreator blockCreator;
    [SerializeField] LayerMask blockLayerMask;
    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform ballSpawnPoint;
    [SerializeField] float ballShootForce = 5f;

    bool canShoot = false;

    // Start is called before the first frame update
    void Start()
    {
        blockCreator.Setup();
        cameraController.OnMoveToStartCompleted += CamSetupCompleted;
    }

    void Update()
    {
        if (canShoot && Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, blockLayerMask))
            {
                // Transform objectHit = hit.transform;
                // Block block = objectHit.GetComponent<Block>();
                // if (block != null && block.isOn)
                // {
                //     block.Remove();
                // }
                Rigidbody ballRBD = Instantiate(ballPrefab,
                    ballSpawnPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
                Vector3 shootDir = hit.transform.position - ballSpawnPoint.position;
                ballRBD.isKinematic = false;
                ballRBD.AddForce(shootDir * ballShootForce, ForceMode.Impulse);
                Destroy(ballRBD.gameObject, 5f);
            }
        }
    }

    void ShootBall()
    {

    }

    void CamSetupCompleted()
    {
        cameraController.OnMoveToStartCompleted -= CamSetupCompleted;
        print("cam setup completed.");
        blockCreator.CamSetupCompleted();
        canShoot = true;
        cameraController.canSwipe = true;
    }
}
