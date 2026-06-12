using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string exitTo;
    [SerializeField] private string sceneToLoad;
    private float waitToLoadTime = 1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() != null)
        {
            SceneManagement.Instance.SetTransitionName(exitTo);

            StartCoroutine(LoadSceneCoroutine());

            Debug.Log(exitTo);
        }
    }
    private IEnumerator LoadSceneCoroutine()
    {
        UI_FadeScreen.Instance.FadeToBlack();
        yield return new WaitForSeconds(waitToLoadTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
