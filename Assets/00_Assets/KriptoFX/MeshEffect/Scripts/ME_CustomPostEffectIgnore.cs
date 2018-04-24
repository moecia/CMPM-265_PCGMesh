using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ME_CustomPostEffectIgnore: MonoBehaviour
{

	// Use this for initialization
	void Start () {
        gameObject.layer = LayerMask.NameToLayer("CustomPostEffectIgnore");
	}
}
