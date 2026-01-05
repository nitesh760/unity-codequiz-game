using System.Collections;
using UnityEngine;

public class SidebarController : MonoBehaviour
{
    public RectTransform sidebarPanel; // Assign your sidebar panel here
    public float slideSpeed = 10f; // Adjust speed as needed
    private bool isOpen = false;
    private float closedPositionX, openPositionX;

    void Start()
    {
        // Set sidebar width dynamically
        float sidebarWidth = sidebarPanel.sizeDelta.x;

        // Get the starting position of the sidebar
        closedPositionX = sidebarPanel.anchoredPosition.x;
        openPositionX = closedPositionX - sidebarWidth; // Move left by its width
    }

    public void OpenSidebar()
    {
        if (!isOpen)
        {
            StartCoroutine(SlidePanel(closedPositionX, openPositionX));
            isOpen = true;
        }
    }

    public void CloseSidebar()
    {
        if (isOpen)
        {
            StartCoroutine(SlidePanel(openPositionX, closedPositionX));
            isOpen = false;
        }
    }

    IEnumerator SlidePanel(float fromX, float toX)
    {
        float elapsedTime = 0f;
        Vector2 startPos = sidebarPanel.anchoredPosition;
        Vector2 targetPos = new Vector2(toX, startPos.y);

        while (elapsedTime < 0.5f)
        {
            elapsedTime += Time.deltaTime * slideSpeed;
            sidebarPanel.anchoredPosition = Vector2.Lerp(startPos, targetPos, elapsedTime);
            yield return null;
        }

        sidebarPanel.anchoredPosition = targetPos;
    }
}
