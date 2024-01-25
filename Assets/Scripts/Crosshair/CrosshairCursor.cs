using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrosshairCursor : MonoBehaviour
{
    [SerializeField] float width;
    [SerializeField] float height;

    // Start is called before the first frame update
    void Awake()
    {
        // Remove cursor
        Cursor.visible= false;
    }

    // Update is called once per frame
    void Update()
    {
        // Ensures that if user clicks on lmb and mouse button is working
        if (Input.GetMouseButtonDown(0) && Time.timeScale != 0)
        {
            // Remove cursor
            Cursor.visible = false;
        }


        Vector3 cursorSize = new Vector3(width, height, 1);

        gameObject.transform.localScale = cursorSize;

        // Get mouse position
        Vector3 mouseCursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        mouseCursorPosition.z = -9f;

        // Set the cursor position
        transform.position = mouseCursorPosition * Time.timeScale;
    }
}
