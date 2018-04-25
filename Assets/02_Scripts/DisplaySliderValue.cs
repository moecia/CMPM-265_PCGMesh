using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class DisplaySliderValue : MonoBehaviour {

    public Slider m_slider;
	// Use this for initialization
	void Start () {
        transform.GetComponent<Text>().text = m_slider.value.ToString();

    }

    // Update is called once per frame
    void Update () {
		
	}

   public void ShowValue()
   {
        transform.GetComponent<Text>().text = m_slider.value.ToString();
   }

}
