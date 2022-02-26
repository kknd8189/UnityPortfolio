using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HpBar : MonoBehaviour
{
    public Persona persona;
    public Slider slider;

    private void Update()
    {
        transform.rotation = Camera.main.transform.rotation;

        float ratio = persona.CurrentHp / (float)persona.MaxHp;
        setHpRatio(ratio);
    } 

    private void setHpRatio(float ratio)
    {
        slider.value = ratio;
    }
}
