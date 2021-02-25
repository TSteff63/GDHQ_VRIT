using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
[ImageEffectAllowedInSceneView]
public class CustomImageEffect : MonoBehaviour {

	// public Material material;

	// [ImageEffectOpaque]
	// void OnRenderImage(RenderTexture src, RenderTexture dest) {
	// 	if (material != null) Graphics.Blit(src, dest, material);
	// }

	public Material _material;

	void OnEnable() {
		// dynamically create a material that will use our shader

		// tell the camera to render depth and normals
	}

	private void Start() {
		_material.SetFloat("_EffectAmount", 0);
	}

	// private void Start() {
	// 	StartCoroutine(ChangeProperty());
	// }

	void OnRenderImage(RenderTexture src, RenderTexture dest) {
		// set shader properties
		GetComponent<Camera>().depthTextureMode |= DepthTextureMode.MotionVectors;
		// _material.SetMatrix("_CamToWorld", GetComponent<Camera>().cameraToWorldMatrix); 
		// execute the shader on input texture (src) and write to output (dest)
		if (_material != null) {
			Graphics.Blit(src, dest, _material);
		}
	}

	public void SetValue(int value) {
		StartCoroutine(SetValueCoroutine(value));
	}

	IEnumerator SetValueCoroutine(int value) {
		float tParam = 0;
		float orValue = _material.GetFloat("_EffectAmount");
		while (tParam < 1) {
			tParam += Time.deltaTime * 4;
			_material.SetFloat("_EffectAmount", Mathf.Lerp(orValue, value, tParam));
			yield return null;
		}
	}

	public void TriggerPropertyChange() {
		StartCoroutine(ChangeProperty());
	}

	IEnumerator ChangeProperty() {
		float tParam = 0;
		while (tParam < 1) {
			tParam += Time.deltaTime * 4;
			_material.SetFloat("_EffectAmount", Mathf.Sin(tParam * Mathf.PI) * 2);
			yield return null;
		}
	}
}