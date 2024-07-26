using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;

    private float waitToLoadTime = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            SceneManagement.Instance.SetTransitionName(sceneTransitionName);

            StartCoroutine(LoadSceneCoroutine());
        }
    }
    private IEnumerator LoadSceneCoroutine()
    {
        Debug.Log("LoadScene called");
        UI_FadeScreen.Instance.FadeToBlack();
        yield return new WaitForSeconds(waitToLoadTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
