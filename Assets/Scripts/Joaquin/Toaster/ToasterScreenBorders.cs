using UnityEngine;

public class ToasterScreenBorders : MonoBehaviour
{
    public float thickness = 1f;
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        CreateBorders();
    }

    void CreateBorders()
    {
        Vector2 screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector2 screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        float screenWidth = screenTopRight.x - screenBottomLeft.x;
        float screenHeight = screenTopRight.y - screenBottomLeft.y;

        // Crear los 4 bordes
        CreateBorder("Left", new Vector2(screenBottomLeft.x - thickness / 2, 0), new Vector2(thickness, screenHeight + 2 * thickness));
        CreateBorder("Right", new Vector2(screenTopRight.x + thickness / 2, 0), new Vector2(thickness, screenHeight + 2 * thickness));
        CreateBorder("Middle", new Vector2((screenTopRight.x + thickness + 3f ) / 2, 0), new Vector2(thickness, screenHeight + 2 * thickness));
        CreateBorder("Top", new Vector2(0, screenTopRight.y + thickness / 2), new Vector2(screenWidth + 2 * thickness, thickness));
        CreateBorder("Bottom", new Vector2(0, screenBottomLeft.y - thickness / 2), new Vector2(screenWidth + 2 * thickness, thickness));
    }

    void CreateBorder(string name, Vector2 center, Vector2 size)
    {
        GameObject border = new GameObject(name);
        border.transform.parent = transform;
        border.transform.position = center;

        // Añadir SpriteRenderer para que se vea
        SpriteRenderer sr = border.AddComponent<SpriteRenderer>();
        sr.sprite = GenerateDefaultWhiteSprite(); // Usa sprite blanco
        sr.color = Color.cyan; // O cualquier color visible
        sr.sortingOrder = 10;

        // Ajustar el tamaño visual
        border.transform.localScale = size;

        // Añadir colisionador
        BoxCollider2D col = border.AddComponent<BoxCollider2D>();
        col.size = Vector2.one;
        col.isTrigger = false;

        // Collider físico
        Rigidbody2D rb = border.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;


        if (name == "Bottom")
        {
            border.tag = "Floor";
        }
    }

    Sprite GenerateDefaultWhiteSprite()
    {
        Texture2D tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, Color.white);
        tex.Apply();
        return Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f));
    }


}
