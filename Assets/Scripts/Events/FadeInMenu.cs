using UnityEngine;

public class FadeInMenu : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;

    [SerializeField]
    private GameObject menu;

    private bool active = false;

    void Update()
    {
        if (active)
        {
            if(canvasGroup.alpha != 1)
            {
                canvasGroup.alpha += 0.1f;
            }
        } 
        else
        {
            if(canvasGroup.alpha != 0) 
            { 
                canvasGroup.alpha -= 0.1f;
            }
            else
            {
                menu.SetActive(false);
            }
        }
    }

    public void ShowMenu()
    {
        canvasGroup.alpha = 0;
        active = true;
        menu.SetActive(true);
    }

    public void HideMenu()
    {
        active = false;
    }
}
