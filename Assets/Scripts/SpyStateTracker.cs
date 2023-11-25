using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpyStateTracker : MonoBehaviour
{
    public GameObject spy;
    public TextMeshProUGUI text;

    private void Update()
    {
        if (spy.GetComponent<RedSpy>() != null)
        {
            var redSpy = spy.GetComponent<RedSpy>();

            text.text = redSpy.state.ToString();
        }
        if(spy.GetComponent<BlueSpy>() != null)
        {
            var blueSpy = spy.GetComponent<BlueSpy>();

            text.text = blueSpy.state.ToString();
        }
    }
}
