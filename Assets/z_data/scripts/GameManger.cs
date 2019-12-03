using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger : MonoBehaviour
{
    [SerializeField] CameraController cameraController;
    [SerializeField] Camera mainCam;
    [SerializeField] BlockCreator blockCreator;
    [SerializeField] LayerMask blockLayerMask;

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
                Transform objectHit = hit.transform;
                Block block = objectHit.GetComponent<Block>();
                print("block " + block.id);
                print("block " + block.isOn);
                if (block != null && block.isOn)
                {
                    block.Remove();
                }
            }
        }
    }

    void CamSetupCompleted()
    {
        cameraController.OnMoveToStartCompleted -= CamSetupCompleted;
        print("cam setup completed.");
        blockCreator.CamSetupCompleted();
        canShoot = true;
    }
}
