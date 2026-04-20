using UnityEngine;

public class WeaponVFXController : MonoBehaviour
{
    [Header("Controls")]
    public KeyCode toggleKey = KeyCode.Space;

    [Header("Main Effects")]
    public ParticleSystem activeVFX;
    public Light weaponLight; // Нове поле для світла

    [Header("One-Shot Effects")]
    public ParticleSystem igniteVFX;
    public ParticleSystem extinguishVFX;

    [Header("Material Settings")]
    public Renderer weaponRenderer;

    private bool isWeaponActive = true;
    private Material weaponMaterial;
    private Color originalEmissionColor;

    private void Start()
    {
        if (weaponRenderer != null)
        {
            weaponMaterial = weaponRenderer.material;
            originalEmissionColor = weaponMaterial.GetColor("_EmissionColor");
        }

        // Встановлюємо початковий стан світла
        if (weaponLight != null) weaponLight.enabled = isWeaponActive;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            ToggleWeapon();
        }
    }

    void ToggleWeapon()
    {
        isWeaponActive = !isWeaponActive;

        if (isWeaponActive)
        {
            if (activeVFX != null) activeVFX.Play();
            if (igniteVFX != null) igniteVFX.Play();
            if (weaponLight != null) weaponLight.enabled = true; // Вмикаємо світло
            SetEmission(true);
        }
        else
        {
            if (activeVFX != null) activeVFX.Stop();
            if (extinguishVFX != null) extinguishVFX.Play();
            if (weaponLight != null) weaponLight.enabled = false; // Вимикаємо світло
            SetEmission(false);
        }
    }

    void SetEmission(bool state)
    {
        if (weaponMaterial != null)
        {
            Color targetColor = state ? originalEmissionColor : Color.black;
            weaponMaterial.SetColor("_EmissionColor", targetColor);

            if (state) weaponMaterial.EnableKeyword("_EMISSION");
            else weaponMaterial.DisableKeyword("_EMISSION");
        }
    }
}