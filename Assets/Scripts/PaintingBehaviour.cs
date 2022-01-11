using UnityEditor.PackageManager;
using UnityEngine;

public class PaintingBehaviour : MonoBehaviour
{
    [SerializeField] private Camera sceneCamera, paintingCamera;
    [SerializeField] private RenderTexture canvasTexture;
    [SerializeField] private uint maxNumberOfBrushes;
    [SerializeField] private Transform brushContainer;
    [SerializeField] private GameObject brushPrefab;

    [SerializeField] private MeshRenderer canvasMeshRenderer;

    [SerializeField] private Material baseMaterial;
    public Vector2 Offset;
    private int brushCounter = 0;

    void Update()
    {
        /*if (Input.GetMouseButton(0))
        {
            SpawnBrushPoint();
        }*/

        /*if (Input.GetKeyDown(KeyCode.R))
        {
            DeleteAllBrushPoints();
        }*/
    }

    void DeleteAllBrushPoints()
    {
        int noPoints = brushContainer.childCount;
        for (int i = 0; i < noPoints; i++)
        {
            Destroy(brushContainer.GetChild(i).gameObject);
        }
    }

    /*private void SpawnBrushPoint()
    {
        Vector3 uvWorldPos = Vector3.zero;

        if (HitTestUVPosition(ref uvWorldPos))
        {
            GameObject brushInstance = Instantiate(brushPrefab);
            brushInstance.transform.parent = brushContainer;
            brushInstance.transform.localPosition = uvWorldPos + Vector3.forward;
        }
    }*/

    public void SpawnBrushPoint(Vector3 origin, Vector3 dir)
    {
        Vector3 uvWorldPos = Vector3.zero;

        if (HitTestUVPosition(ref uvWorldPos, new Ray(origin, dir)))
        {
            GameObject brushInstance = Instantiate(brushPrefab);
            brushInstance.transform.parent = brushContainer;
            brushInstance.transform.localPosition = uvWorldPos + Vector3.forward;
            /*brushCounter++;
            if (brushCounter > maxNumberOfBrushes)
            {
                MergeTexture();
            }*/
        }
    }

    /*private bool HitTestUVPosition(ref Vector3 uvWorldPosition)
    {
        RaycastHit hit;
        Vector3 cursorPos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.0f);
        Ray cursorRay = sceneCamera.ScreenPointToRay(cursorPos);
        LayerMask mask = LayerMask.GetMask("Paintable");
        if (Physics.Raycast(cursorRay, out hit, 10, mask))
        {
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
                return false;
            Vector2 pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
            Debug.Log("World Texture Coords:" + pixelUV);
            uvWorldPosition.x = pixelUV.x * 10 - Offset.x;// - paintingCamera.orthographicSize; //To center the UV on X
            uvWorldPosition.y = pixelUV.y * 10 - Offset.y;// - paintingCamera.orthographicSize; //To center the UV on Y
            uvWorldPosition.z = 0.0f;
            return true;
        }
        return false;
    }*/

    public bool HitTestUVPosition(ref Vector3 uvWorldPosition, Ray givenRay)
    {
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Paintable");
        if (Physics.Raycast(givenRay, out hit, 10, mask))
        {
            MeshCollider meshCollider = hit.collider as MeshCollider;
            if (meshCollider == null || meshCollider.sharedMesh == null)
                return false;

            // Check if the texture we're drawing unto now is different
            MeshRenderer objRenderer = hit.transform.GetComponent<MeshRenderer>();
            Material objMatInstance = objRenderer.material;
            if (objRenderer.material != baseMaterial)
            {
                // Bake the decals into the old texture if there's one
                MergeTexture();
                // Get the new texture of the object hit
                baseMaterial = objMatInstance;
                
                canvasMeshRenderer.material = objMatInstance;
                canvasMeshRenderer.material.mainTexture = objMatInstance.mainTexture;
                objMatInstance.mainTexture = canvasTexture;
            }


            // Get the UV coord to world space
            Vector2 pixelUV = new Vector2(hit.textureCoord.x, hit.textureCoord.y);
            Debug.Log("World Texture Coords:" + pixelUV);
            // Multiply it by then because from 0.0 - 1.0 we need to scale it to 0.0 - 10.0 (the plane is 10 units)
            uvWorldPosition.x = pixelUV.x * 10 - Offset.x; // - paintingCamera.orthographicSize; //To center the UV on X
            uvWorldPosition.y = pixelUV.y * 10 - Offset.y; // - paintingCamera.orthographicSize; //To center the UV on Y
            uvWorldPosition.z = 0.0f;
            return true;
        }
        return false;
    }

    private void MergeTexture()
    {
        RenderTexture.active = canvasTexture;
        int width = canvasTexture.width;
        int height = canvasTexture.height;
        Texture2D tex = new Texture2D(width, height, TextureFormat.RGB24, false);
        tex.ReadPixels(new Rect(0,0, width, height),0,0);
        tex.Apply();
        RenderTexture.active = null;
        baseMaterial.mainTexture = tex;
        DeleteAllBrushPoints();
        brushCounter = 0;
    }
}