using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testShadowSprite : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<SpriteRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        this.GetComponent<SpriteRenderer>().receiveShadows = true;
    }


}
