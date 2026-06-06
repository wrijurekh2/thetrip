using UnityEngine;
using UnityEngine.SceneManagement;

public class ShowInfoButton : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject infoScreen;
    void OnMouseDown()
    {
        mainMenu.SetActive(false);
        infoScreen.SetActive(true);
    }
}
