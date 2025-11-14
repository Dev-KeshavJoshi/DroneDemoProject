using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SwipeDetection : MonoBehaviour
{
    [SerializeField] private InputAction position, press;
    [SerializeField] private ScrollRect Vertical_ScrollView;
    [SerializeField] private ScrollRect[] Horizontal_ScrollViews;

    [SerializeField] private float swipeResistance = 30;
    private Vector2 initialPos;
    private Vector2 currentPos => position.ReadValue<Vector2>();

    private void Awake()
    {
        Debug.Log("Testing Swipe");

        position.Enable();
        press.Enable();
        /*press.performed += _ => { 
            initialPos = currentPos;
        };
        press.canceled += _ =>{
            DetectSwipe();
        };*/
    }

    void Update()
    {
        // Check if there is at least one touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            initialPos = touch.position;

            if (touch.phase == UnityEngine.TouchPhase.Moved)
            {
                Vector2 delta = currentPos - initialPos;

                if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y) && Mathf.Abs(delta.x) > swipeResistance)
                {
                    //Vertical_ScrollView.enabled = false;
                    for(int i = 0; i < Horizontal_ScrollViews.Length; i++)
                    {
                        Horizontal_ScrollViews[i].enabled = true;
                    }
                }
                else if(Mathf.Abs(delta.y) > Mathf.Abs(delta.x) && Mathf.Abs(delta.y) > swipeResistance)
                {
                    //Vertical_ScrollView.enabled = true;
                    for (int i = 0; i < Horizontal_ScrollViews.Length; i++)
                    {
                        Horizontal_ScrollViews[i].enabled = false;
                    }
                }
            }
        }
    }

    private void DetectSwipe()
    {
        Vector2 delta = currentPos - initialPos;
        Vector2 direction = Vector2.zero;

        if (Mathf.Abs(delta.y) > swipeResistance)
        {
            direction.y = Mathf.Clamp(delta.y, -1, 1);
        }
        if (direction != Vector2.zero)
        {
            //Horizontal_ScrollView.horizontal = true;
            Debug.Log("Enable Horizontal_ScrollView");
        }
    }

    public void DisableHorizontalScrollView(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //Horizontal_ScrollView.horizontal = false;
            Debug.Log("Disabled Horizontal_ScrollView");
        }
        else if (context.canceled)
        {
            //Horizontal_ScrollView.horizontal = true;
            Debug.Log("Enable Horizontal_ScrollView");
        }
    }
}