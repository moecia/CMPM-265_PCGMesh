using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeshManager : MonoBehaviour {

    public CubeMaker m_cubeMaker;
    public SphereMaker m_sphereMaker;

    public Slider rowSlider;
    public Slider colSlider;
    public Slider sizeSlider;
    public Slider heightSlider;
    public Slider speedSlider;
    public Slider DistanceSlider;

    // Use this for initialization
    void Start ()
    {
        rowSlider.value = m_cubeMaker.row;
        colSlider.value = m_cubeMaker.col;

        if (m_cubeMaker.transform.GetComponent<MeshRenderer>() &&
            m_cubeMaker.transform.GetComponent<MeshRenderer>().enabled)
        {
            sizeSlider.value = m_cubeMaker.sizeMultiplier;
            speedSlider.value = m_cubeMaker.animateSpeed;
        }
        if (m_sphereMaker.transform.GetComponent<MeshRenderer>() &&
            m_sphereMaker.transform.GetComponent<MeshRenderer>().enabled)
        {
            sizeSlider.value = m_sphereMaker.radius;
            speedSlider.value = m_sphereMaker.animateSpeed;
        }

        heightSlider.value = m_cubeMaker.oriHeight;

        DistanceSlider.value = m_cubeMaker.distance;
    }

    private void Update()
    {
        if (m_cubeMaker.transform.GetComponent<MeshRenderer>() &&
            m_cubeMaker.transform.GetComponent<MeshRenderer>().enabled)
        {
            if(sizeSlider.value != m_cubeMaker.sizeMultiplier)
                sizeSlider.value = m_cubeMaker.sizeMultiplier;
            if (speedSlider.value != m_cubeMaker.animateSpeed)
                speedSlider.value = m_cubeMaker.animateSpeed;
        }

        if (m_sphereMaker.transform.GetComponent<MeshRenderer>() &&
            m_sphereMaker.transform.GetComponent<MeshRenderer>().enabled)
        {
            if(sizeSlider.value != m_sphereMaker.radius)
                sizeSlider.value = m_sphereMaker.radius;
            if (speedSlider.value != m_sphereMaker.animateSpeed)
                speedSlider.value = m_sphereMaker.animateSpeed;
        }
            
    }

    public void ChangeRow()
    {
        m_cubeMaker.row = (int)rowSlider.value;
    }

    public void ChangeCol()
    {
        m_cubeMaker.col = (int)colSlider.value;
    }

    public void ChangeSize()
    {
        if (m_cubeMaker.transform.GetComponent<MeshRenderer>() &&
            m_cubeMaker.transform.GetComponent<MeshRenderer>().enabled)
            m_cubeMaker.sizeMultiplier = sizeSlider.value;

        if (m_sphereMaker.transform.GetComponent<MeshRenderer>() &&
            m_sphereMaker.transform.GetComponent<MeshRenderer>().enabled)
            m_sphereMaker.radius = sizeSlider.value;
    }

    public void ChangeHeight()
    {
        m_cubeMaker.oriHeight = heightSlider.value;
    }

    public void ChangeSpeed()
    {
        if (m_cubeMaker.transform.GetComponent<MeshRenderer>() &&
            m_cubeMaker.transform.GetComponent<MeshRenderer>().enabled)
            m_cubeMaker.animateSpeed = (int)speedSlider.value;

        if (m_sphereMaker.transform.GetComponent<MeshRenderer>() &&
            m_sphereMaker.transform.GetComponent<MeshRenderer>().enabled)
            m_sphereMaker.animateSpeed = (int)speedSlider.value;
    }

    public void ChangeDistance()
    {
        m_cubeMaker.distance = DistanceSlider.value;
    }

    public void CanAnimate()
    {
        if (m_cubeMaker.transform.GetComponent<MeshRenderer>() &&
            m_cubeMaker.transform.GetComponent<MeshRenderer>().enabled)
            m_cubeMaker.canBeAnimated = !m_cubeMaker.canBeAnimated;

        if (m_sphereMaker.transform.GetComponent<MeshRenderer>() &&
            m_sphereMaker.transform.GetComponent<MeshRenderer>().enabled)
            m_sphereMaker.canBeAnimated = !m_sphereMaker.canBeAnimated;
    }

    public void AnimateVFX()
    {
        m_cubeMaker.animateVFX = !m_cubeMaker.animateVFX;
    }
}
