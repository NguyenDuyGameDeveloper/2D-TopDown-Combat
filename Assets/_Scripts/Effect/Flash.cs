using System.Collections;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material whiteFlashMat;
    [SerializeField] private float restoreDefaultMatTime = .2f;

    private Material defaultMat;
    private SpriteRenderer sr;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        defaultMat = sr.material;
    }
    public float GetRestoreMatTime() { return restoreDefaultMatTime; }  
    public IEnumerator FlashCoroutine()
    {
        sr.material = whiteFlashMat;
        yield return new WaitForSeconds(restoreDefaultMatTime);
        sr.material = defaultMat;
    }
}
