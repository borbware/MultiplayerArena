using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerPulse : MonoBehaviour
{
    // Start is called before the first frame update
    LineRenderer lr;
    Color GradientColor;

    [SerializeField] float PulseRate;

    int CurState = 0;

    [SerializeField] Color BaseCol, PulseColor;

    Gradient gradientCol1 = new Gradient();
    Gradient gradientCol2 = new Gradient();

     float LastPulse = 0;

    void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));

                gradientCol1.SetKeys(
            new GradientColorKey[] { new GradientColorKey(BaseCol, 0.0f), 
            new GradientColorKey(BaseCol, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(BaseCol.a, 0.0f), 
            new GradientAlphaKey(BaseCol.a, 1.0f) }
        );
        gradientCol2.SetKeys(
            new GradientColorKey[] { new GradientColorKey(PulseColor, 0.0f), 
            new GradientColorKey(PulseColor, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(PulseColor.a, 0.0f), 
            new GradientAlphaKey(PulseColor.a, 1.0f) });

        lr.colorGradient = gradientCol1;

    }

    void Update()
    {
        if(Time.time-PulseRate > LastPulse){
            LastPulse = Time.time;
            ColPulse();
        }
    }


    void ColPulse(){
            if(CurState != 1){
                CurState = 1;
                lr.colorGradient = gradientCol2;
            } else{
                CurState = 0;
                lr.colorGradient = gradientCol1;
            }
    }
}
