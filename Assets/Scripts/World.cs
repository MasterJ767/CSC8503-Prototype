using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
    private const int width = 20;
    private const int height = 20;
    private const int cellWidth = 5;

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    private List<Vector3> vertices;
    private List<int> triangles;
    private List<Color> colours;
    private Color[,] map;

    private void Start() {
        meshFilter = GetComponent<MeshFilter>();
        meshRenderer = GetComponent<MeshRenderer>();
        vertices = new List<Vector3>();
        triangles = new List<int>();
        colours = new List<Color>();
        map = new Color[width, height];
        for (int z = 0; z < height; ++z) {
            for(int x = 0; x < width; ++x) {
                map[x, z] = Color.white;
            }
        }
        Triangulate();
    }

    public void ChangeCell(Vector3 globalPosition, Color colour) {
        map[Mathf.FloorToInt(globalPosition.x / cellWidth), Mathf.FloorToInt(globalPosition.z / cellWidth)] = colour;
        Triangulate();
    }

    private void Triangulate() {
        int i = 0;

        for (int z = 0; z < height; ++z) {
            for(int x = 0; x < width; ++x) {
                vertices.Add(new Vector3(x * cellWidth, 0, z * cellWidth));
                vertices.Add(new Vector3(x * cellWidth, 0, (z + 1) * cellWidth));
                vertices.Add(new Vector3((x + 1) * cellWidth, 0, (z + 1) * cellWidth));
                vertices.Add(new Vector3((x + 1) * cellWidth, 0, z * cellWidth));

                triangles.Add(i);
                triangles.Add(i + 1);
                triangles.Add(i + 2);
                triangles.Add(i);
                triangles.Add(i + 2);
                triangles.Add(i + 3);

                colours.Add(map[x, z]);
                colours.Add(map[x, z]);
                colours.Add(map[x, z]);
                colours.Add(map[x, z]);

                i += 4;
            }
        }

        Mesh mesh = new Mesh();
        mesh.SetVertices(vertices.ToArray());
        mesh.SetTriangles(triangles.ToArray(), 0);
        mesh.SetColors(colours.ToArray());
        meshFilter.mesh = mesh;
    }
}
