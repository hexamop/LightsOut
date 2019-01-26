﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class DealDamage : MonoBehaviour {

    public int damage = 5;

    public Renderer[] renderers;

    WaitForFixedUpdate waitFixedUpdate = new WaitForFixedUpdate();

    float fadeSpeed = 0.01f;
    float fadeVal;

    Color fadeColor;

    bool isFading;


   //Shader standardShader;

    //Material fadeMat;
    //Material defoultMat;



    void Start () {
        renderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer rend in renderers)
        {
            rend.sharedMaterial.shader = Shader.Find("Standard");
            ToRegularMat(rend.material);
        }

        //fadeShader = Shader.Find("Legacy Shaders/Transparent/VertexLit");
        //standardShader = renderers[0].material.shader;


    }




    public void HideObj()
    {
        foreach(Renderer rend in renderers)
        {
            rend.enabled = false;
        }
    }



    public void ShowObj()
    {
        foreach (Renderer rend in renderers)
        {
            rend.enabled = true;
        }
    }


    public void FadeRed()
    {
        if (GameManager.instance.lightsOn) return;

        if (isFading) return;

        isFading = true;
        StartCoroutine(FadeRedCo());

    }


    IEnumerator FadeRedCo()
    {

        ShowObj();
        foreach (Renderer rend in renderers)
        {
            ToFadeMat(rend.material);
        }

        fadeVal = 1;
        while (fadeVal > 0)
        {
            fadeVal -= fadeSpeed;
            foreach (Renderer rend in renderers)
            {
                fadeColor = rend.material.color;
                fadeColor.a = fadeVal;
                rend.material.color = fadeColor;
            }

            yield return waitFixedUpdate;
        }

        HideObj();
        foreach (Renderer rend in renderers)
        {
            ToRegularMat(rend.material);
            //fadeColor = rend.material.color;
            fadeColor.a = 1f;
            rend.material.color = fadeColor;
        }
        isFading = false;
    }


    void ToFadeMat(Material mat)
    {
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;
        mat.EnableKeyword("_EMISSION");
        mat.SetColor("_EmissionColor", Color.red);
    }

    void ToRegularMat(Material mat)
    {
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        mat.SetInt("_ZWrite", 1);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.DisableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = -1;
        mat.DisableKeyword("_EMISSION");
    }

}
