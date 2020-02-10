using System;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class SpecialDemo : MonoBehaviour
{
    private string[] text = {
        "Wake up, Neo...",
        "The Matrix has you",
        "Follow the white rabbit.",
        "Knock, knock, Neo."
    };

    private Tile[] tiles;

    [SerializeField]
    private Tilemap tilemap;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        var objects = AssetDatabase.LoadAllAssetRepresentationsAtPath("Assets/03. Programmatic Tilemap/Fonts/terminal16x16_gs_ro.png");
        this.tiles = Array.ConvertAll(objects, item =>
        {
            var glyph = (Sprite)item;

            var tile = ScriptableObject.CreateInstance<Tile>();
            tile.sprite = glyph;
            tile.color = Color.green;
            
            return tile;
        });

        var lineWait = 1.5f;

        foreach(var line in this.text)
        {
            yield return new WaitForSeconds(lineWait);

            this.tilemap.ClearAllTiles();
            foreach(var val in this.RenderLine(line))
            {
                yield return val;
            }

            lineWait += 0.2f;
        }

        yield return new WaitForSeconds(1f);
        
        this.tilemap.ClearAllTiles();
        
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        this.tilemap.ClearAllTiles();
        foreach(var val in this.RenderLine("What questions do you have?"))
        {
            yield return val;
        }
        
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        this.tilemap.ClearAllTiles();

        foreach(var val in this.RenderLine("Thanks!", 0.05f, 0.1f))
        {
            yield return val;
        }

        foreach(var val in this.RenderLine("@LazyGameDevZA", 0.05f, 0.1f, 2, 44))
        {
            yield return val;
        }

        foreach(var val in this.RenderLine("sas@lazygamedev.co.za", 0.05f, 0.1f, 2, 42))
        {
            yield return val;
        }

        foreach(var val in this.RenderLine("https://github.com/LazyGameDevZA/MGSA-Roguelike-Terminals", 0.05f, 0.1f, 1, 1))
        {
            yield return val;
        }
    }

    private IEnumerable RenderLine(string line, float minSeconds = 0.1f, float maxSeconds = 0.15f, int x = 1, int y = 48)
    {
        for(int i = 0; i < line.Length; i++)
        {
            var character = line[i];
            var tile = this.tiles[(byte)character];

            this.tilemap.SetTile(new Vector3Int(x + i, y, 0), tile);

            yield return new WaitForSeconds(Random.Range(minSeconds, maxSeconds));
        }
    }
}
