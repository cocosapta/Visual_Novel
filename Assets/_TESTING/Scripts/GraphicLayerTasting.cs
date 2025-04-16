using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicLayerTasting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Running());

    }

    IEnumerator Running()
    {
        GraphicPanel panel = GraphicPanelManager.instance.GetPanel("Background");
        GraphicLayer layer = panel.GetLayer(0, true);

        yield return new WaitForSeconds(1);

        Texture blendText = Resources.Load<Texture>("Graphics/Transition Effects/hurricane");
        layer.SetTexture("Graphics/BG Images/gerbang", blendingTexture : blendText);

        yield return new WaitForSeconds(1);
        layer.currentGraphic.FadeOut();
        yield return new WaitForSeconds(2);

        Debug.Log(layer.currentGraphic);

    }
}
