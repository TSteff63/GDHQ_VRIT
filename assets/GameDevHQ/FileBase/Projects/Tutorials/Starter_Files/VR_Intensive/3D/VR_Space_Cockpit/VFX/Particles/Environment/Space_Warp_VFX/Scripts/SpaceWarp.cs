using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceWarp : MonoBehaviour {
    public Material radialBlur;
    public Material tunnelMat;
    public Material skyboxMat;

    public AnimationCurve startCurve = new AnimationCurve();
    public AnimationCurve endCurve = new AnimationCurve();

    public void Warp() {
        StartCoroutine(WarpCoroutine());
        StartCoroutine(TunnelCoroutine());
    }

    private void Update() {
        if (Input.GetButtonDown("Jump")) {
            Warp();
        }
    }

    IEnumerator WarpCoroutine() {
        float t = 0;
        while (t < 1.0f) {
            t += Time.deltaTime;
            radialBlur.SetFloat("_EffectAmount", Mathf.Lerp(0.0f, 5.0f, startCurve.Evaluate(t)));
            yield return null;
        }
    }

    IEnumerator TunnelCoroutine() {
        yield return new WaitForSeconds(0.5f);
        float t = 0;
        while (t < 0.5f) {
            t += Time.deltaTime;
            tunnelMat.SetFloat("_Cutoff", Mathf.Lerp(1.0f, 0.0f, t / 0.5f));
            yield return null;
        }
        yield return new WaitForSeconds(5);
        t = 0;
        RenderSettings.skybox = skyboxMat;
        while (t < 0.5f) {
            t += Time.deltaTime;
            tunnelMat.SetFloat("_Cutoff", Mathf.Lerp(0.0f, 1.0f, t / 0.5f));
            radialBlur.SetFloat("_EffectAmount", endCurve.Evaluate(t / 0.5f) * 5.0f);
            yield return null;
        }
    }
}