using UnityEditor;
using UnityEngine;

public class MapManagerWizard : ScriptableWizard
{
    public string Name = "Map";
    public float YPosition = 0;
    public int TextureResolution = 1024;
    public int NumberOfTiles = 16;
    public float SizeOfCell = 16;
    public string Shader = "Diffuse";
    public TileSet TileTexture;
    public EntitySet Entity;

    public void OnWizardUpdate()
    {
        helpString = "Creates a new map layer.";
        if (Name == string.Empty)
        {
            errorString = "Please assign a name";
            isValid = false;
        }
        else if (TextureResolution > 4096)
        {
            errorString = "Texture Resolution must be 4096 or less";
            isValid = false;
        }
        else if (!IsPowerOf2(TextureResolution))
        {
            errorString = "Texture Resolution must be a power of 2";
            isValid = false;
        }
        else if (NumberOfTiles > 32)
        {
            errorString = "Number of Tiles must be 32 or less";
            isValid = false;
        }
        else if (!IsPowerOf2(NumberOfTiles))
        {
            errorString = "Number of Tiles must be a power of 2";
            isValid = false;
        }
        else if (SizeOfCell <= 0)
        {
            errorString = "Size of cell must be greater than 0";
            isValid = false;
        }
        else if (UnityEngine.Shader.Find(Shader) == null)
        {
            errorString = "Shader doesn't exist";
            isValid = false;
        }
        else if (TileTexture == null)
        {
            errorString = "Please assign a TextureSet";
            isValid = false;
        }
        else
        {
            errorString = "";
            isValid = true;
        }
    }

    public void OnWizardCreate()
    {
        var go = new GameObject(Name);
        go.transform.position = new Vector3(0, YPosition, 0);
        var mm = go.AddComponent<MapManager>();
        mm.TextureResolution = TextureResolution;
        mm.NumberOfTiles = NumberOfTiles;
        mm.SizeOfCell = SizeOfCell;
        mm.DefaultShader = Shader;
        mm.TileTextureSet = TileTexture;
        mm.EntitySet = Entity;

        Selection.activeGameObject = go;
    }

    private bool IsPowerOf2(int x)
    {
        return (x > 0) && ((x & (x - 1)) == 0);
    }
}