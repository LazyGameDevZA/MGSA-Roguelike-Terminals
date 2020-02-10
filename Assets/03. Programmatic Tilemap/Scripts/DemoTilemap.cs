using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class DemoTilemap : MonoBehaviour
{
    private Sprite[] glyphs;
    
    [SerializeField]
    private Tilemap tilemap;
    
    // Start is called before the first frame update
    void Start()
    {
        var objects = AssetDatabase.LoadAllAssetRepresentationsAtPath("Assets/03. Programmatic Tilemap/Fonts/terminal16x16_gs_ro.png");
        this.glyphs = Array.ConvertAll(objects, item => (Sprite)item);
    }

    public void PlaceRandomAt()
    {
        var glyph = this.glyphs['@'];

        var tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = glyph;
        tile.color = Color.yellow;

        var x = Random.Range(0, 40);
        var y = Random.Range(0, 25);

        this.tilemap.SetTile(new Vector3Int(x, y, 0), tile);
    }

    public void FillRandom()
    {
        StartCoroutine(this.FillRandomEnumerator());
    }

    private IEnumerator FillRandomEnumerator()
    {
        for(int x = 0; x < 40; x++)
        {
            for(int y = 0; y < 25; y++)
            {
                var glyph = this.glyphs[Random.Range(0, this.glyphs.Length)];

                var tile = ScriptableObject.CreateInstance<Tile>();
                tile.sprite = glyph;
                tile.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
                
                this.tilemap.SetTile(new Vector3Int(x, y, 0), tile);
            }
            yield return new WaitForEndOfFrame();
        }
    }
}
