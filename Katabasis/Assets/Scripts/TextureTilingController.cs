using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class TextureTilingController : MonoBehaviour {

    // Give us the texture so that we can scale proportianally the width according to the height variable below
    // We will grab it from the meshRenderer
    public Texture texture;
    public float textureToMeshY = 2f; // Use this to contrain texture to a certain size

    Vector3 prevScale = Vector3.one;
    float prevTextureToMeshY = -1f;

    // Use this for initialization
    void Start () {
		// texture = gameObject.texture;
        this.prevScale = gameObject.transform.lossyScale;
        this.prevTextureToMeshY = this.textureToMeshY;

        this.UpdateTiling();
    }

    // Update is called once per frame
    void Update () {
        // If something has changed
        if(gameObject.transform.lossyScale != prevScale || !Mathf.Approximately(this.textureToMeshY, prevTextureToMeshY))
            this.UpdateTiling();

        // Maintain previous state variables
        this.prevScale = gameObject.transform.lossyScale;
        this.prevTextureToMeshY = this.textureToMeshY;
    }

    [ContextMenu("UpdateTiling")]
    void UpdateTiling()
    {
        // A Unity plane is 10 units x 10 units
        float planeSizeX = 10f;
        float planeSizeY = 10f;

        // Figure out texture-to-mesh width based on user set texture-to-mesh height
        float textureToMeshX = ((float)this.texture.width/this.texture.height)*this.textureToMeshY;

        gameObject.GetComponent<Renderer>().material.mainTextureScale = new Vector2(planeSizeX*gameObject.transform.lossyScale.x/textureToMeshX, planeSizeY*gameObject.transform.lossyScale.y/textureToMeshY);
    }
}