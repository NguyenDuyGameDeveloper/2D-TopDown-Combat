using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Stamina : Singleton<Stamina>
{
    #region Info
    public int CurrentStamina { get; private set; }

    [SerializeField] private Sprite fullStaminaImage, emptyStaminaImage;
    [SerializeField] private float timeBetweenStaminaRestore = 5;

    private Transform staminaContainer;
    private int startingStamina = 3;
    private int maxStamina;

    const string STAMINA_CONTAINER_TEXT = "Stamina Container";
    #endregion
    protected override void Awake()
    {
        base.Awake();
        maxStamina = startingStamina;
        CurrentStamina = startingStamina;
    }
    private void Start()
    {
        staminaContainer = GameObject.Find(STAMINA_CONTAINER_TEXT).transform;
    }
    public void ReplenishStaminaOnDeath()
    {
        CurrentStamina = startingStamina;
        UpdateStaminaImages();
        CameraController.Instance.SetPlayerCameraFollow();
    }
    public void UseStamina()
    {
        CurrentStamina--;
        UpdateStaminaImages();
        StopAllCoroutines();
        StartCoroutine(RestoreStaminaOverTimeCoroutine());
    }
    public void RestoreStamina()
    {
        if (CurrentStamina < maxStamina && !PlayerHealth.Instance.IsDead)
            CurrentStamina++;
        UpdateStaminaImages();
    }
    private IEnumerator RestoreStaminaOverTimeCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenStaminaRestore);
            RestoreStamina();
        }
    }
    private void UpdateStaminaImages()
    {
        for (int i = 0; i < maxStamina; i++)
        {
            Transform child = staminaContainer.GetChild(i);
            Image image = child?.GetComponent<Image>();

            if (i <= CurrentStamina - 1)
                image.sprite = fullStaminaImage;
            else
                image.sprite = emptyStaminaImage;
        }
    }
}
