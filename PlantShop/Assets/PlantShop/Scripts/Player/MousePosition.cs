using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MousePosition : MonoBehaviour
{
    public static MousePosition Instance;
    [SerializeField] public Camera camera;
    [SerializeField] LayerMask interactionLayer;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] bool canInteract;
    [SerializeField] Vector3 point;
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerToolManager.Instance.playerImage.transform.position = Mouse.current.position.ReadValue();
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        //Debug.Log(ray);
        if (Physics.Raycast(ray, out RaycastHit raycastHitGeneral, float.MaxValue, groundLayer))
        {
            point = raycastHitGeneral.point;
            transform.position = raycastHitGeneral.point;
            if (Physics.Raycast(ray, out RaycastHit raycastHitInteract, float.MaxValue, interactionLayer))
            {
                canInteract = true;
            }
            else
            {
                canInteract = false;
            }
        }
    }

}
