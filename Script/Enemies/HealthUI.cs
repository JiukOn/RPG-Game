using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterStats))]
public class HealthUI : MonoBehaviour
{
    public GameObject uiPrefab;
    public Transform target;

    Transform ui;
    Transform cam;    
    Image healthSlider;
    float visibleTime = 5;
    float lastvisibleTime;
    
    
    void Start()
    {
        cam = Camera.main.transform;
        foreach(Canvas c in FindObjectsOfType<Canvas>()){
            if(c.renderMode == RenderMode.WorldSpace){
                ui = Instantiate(uiPrefab,c.transform).transform;
                healthSlider = ui.GetChild(0).GetComponent<Image>();
                ui.gameObject.SetActive(false);
                break;
            }
        }
        GetComponent<CharacterStats>().OnHealthChanged += OnHealthChanged;
    }

    void OnHealthChanged(int maxHealth,int currentHealth){
        if(ui != null){
            ui.gameObject.SetActive(true);

            lastvisibleTime = Time.time;

            float healthPercent = (float)currentHealth / maxHealth;
            healthSlider.fillAmount = healthPercent;
            if(currentHealth <= 0){
                Destroy(ui.gameObject);
            }
        }
    }
    void LateUpdate()
    {
        if(ui != null){
            ui.position = target.position;
            ui.forward = -cam.forward;

            if(Time.time - lastvisibleTime > visibleTime){
                ui.gameObject.SetActive(false);
            }
        }
    }
}
