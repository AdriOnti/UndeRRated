using UnityEngine;

public class RainbowEffect : MonoBehaviour
{
    public float rainbowSpeed;
    public bool invert;
    public bool randomize;

    private float hue;
    private float sat;
    private float bri;
    private Renderer meshRenderer;
    private new Light light;


    // Start is called before the first frame update
    void Start()
    {

        light = GetComponent<Light>();  
        meshRenderer = GetComponent<Renderer>();
        if (randomize)
        {
            hue = Random.Range(0f, 1f);
        }
        sat = 1;
        bri = 1;
        meshRenderer.material.color = Color.HSVToRGB(hue, sat, bri);
        light.color = Color.HSVToRGB(hue, sat, bri);

    }

    // Update is called once per frame
    void Update()
    {
        Color.RGBToHSV(meshRenderer.material.color, out hue, out sat, out bri);
        Color.RGBToHSV(light.color, out hue, out sat, out bri);

        if (invert)
        {
            hue -= rainbowSpeed / 10000;
            if (hue <= 1)
            {
                hue = 0;
            }
            meshRenderer.material.color = Color.HSVToRGB(hue, sat, bri);
            light.color = Color.HSVToRGB(hue, sat, bri);
        }
        else
        {
            hue += rainbowSpeed / 10000;
            if (hue >= 1)
            {
                hue = 0;
            }
            meshRenderer.material.color = Color.HSVToRGB(hue, sat, bri);
            light.color = Color.HSVToRGB(hue, sat, bri);
        }
        


    }
}
