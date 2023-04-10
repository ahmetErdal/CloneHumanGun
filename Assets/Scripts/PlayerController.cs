using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HumanGunCase.Managers;

public class PlayerController : MonoBehaviour
{
    #region Singleton
    public static PlayerController instance = null;
    private void Awake()
    {
        if (instance == null) instance = this;

    }
    #endregion
    public float maxHorizontalSpeed;
    public float maxForwardSpeed;
    [SerializeField] float horiziontalClamp;

    [SerializeField] Animator playerAnimator;

    private float firstPositionX;
    private float offsetX;

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isGameStarted)
            return;
        
        SetTouchControl();
        SetMouseControl();


        AssignMovement(offsetX * Time.deltaTime * maxHorizontalSpeed);
    }
    private void SetTouchControl()
    {
        if (Input.touchCount <= 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            //FirstTouch();
            firstPositionX = touch.position.x;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
            offsetX = touch.position.x - firstPositionX;
            firstPositionX = touch.position.x;
        }
        else if (touch.phase == TouchPhase.Ended)
        {
            offsetX = 0;
        }
    }
    private void SetMouseControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //FirstTouch();
            firstPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButton(0))
        {
            offsetX = Input.mousePosition.x - firstPositionX;
            firstPositionX = Input.mousePosition.x;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            offsetX = 0;
        }
    }
    private void AssignMovement(float xDisplacement)
    {
        transform.position = (new Vector3(
            Mathf.Clamp(transform.position.x + (250 * xDisplacement) / Screen.width, -horiziontalClamp, horiziontalClamp),
            transform.position.y,
            transform.position.z + maxForwardSpeed * Time.deltaTime));
    }
}
