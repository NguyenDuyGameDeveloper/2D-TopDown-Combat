using System.Collections;
using UnityEngine;

public class AreaEnter : MonoBehaviour
{
    [SerializeField] private string enterFrom;

    private IEnumerator Start()
    {
        yield return null;

        if (enterFrom == SceneManagement.Instance.SceneTransitionName)
        {

            PlayerController.Instance.transform.position = this.transform.position;
            CameraController.Instance.SetPlayerCameraFollow();

            UI_FadeScreen.Instance.FadeToClear();

        }
    }
}
