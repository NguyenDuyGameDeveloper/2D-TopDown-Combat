using UnityEngine;
using UnityEngine.UI;

public class CursorManager : MonoBehaviour
{
    private Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }
    private void Start()
    {
        CursorSetting();
    }
    private void Update()
    {
        SetMousePos();
    }
    private void FixedUpdate()
    {
        EditorCursorVisible();
    }
    private void EditorCursorVisible()
    {
        if (!Application.isPlaying)
            return;
        Cursor.visible = false;
    }
    private void SetMousePos()
    {
        Vector2 cursorPos = Input.mousePosition;
        image.rectTransform.position = cursorPos;
    }
    private void CursorSetting()
    {
        Cursor.visible = false;

        if (Application.isPlaying)
            Cursor.lockState = CursorLockMode.None;
        else
            Cursor.lockState = CursorLockMode.Confined;
    }
}
