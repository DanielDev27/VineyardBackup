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
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerToolManager.Instance.playerImage.transform.position = Mouse.current.position.ReadValue();
        Ray ray = camera.ScreenPointToRay(Mouse.current.position.ReadValue());
        //Debug.Log(ray);
        if (Physics.Raycast(ray, out RaycastHit raycastHitGeneral, Mathf.Infinity, groundLayer))
        {
            point = raycastHitGeneral.point;
            transform.position = point;
            Debug.DrawRay(ray.origin, point - ray.origin, Color.black);
            if (Physics.Raycast(ray, out RaycastHit raycastHitInteract, Mathf.Infinity, interactionLayer))
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
