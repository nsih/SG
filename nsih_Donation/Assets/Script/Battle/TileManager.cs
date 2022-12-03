// ����
// Ÿ���� ������ �����ϴ� Ŭ����
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Dictionary<Vector3Int, bool> objectOnTile; // ������Ʈ�� Ÿ�� ���� �ִ°�?�� ���� ����
    public Dictionary<Vector3Int, int> objectCount;// Ÿ�� ���� �ִ� ������Ʈ ������ ���� ����
    public Tilemap tilemap; // Ÿ�ϸ� ������Ʈ

    private void Start()
    {
        // Ÿ�ϸ� ���� �� ��ǥ�鿡 ���ؼ� Ÿ���� �ִٸ� ��ųʸ��� �ʱ� ������ �߰��Ѵ�.
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            // �ش� ��ǥ�� Ÿ���� ������ �Ѿ��.
            if (!tilemap.HasTile(pos)) continue;
            // �ش� ��ǥ�� Ÿ���� ��´�.
            var tile = tilemap.GetTile<TileBase>(pos);
            // ���� �ʱ�ȭ
            objectOnTile.Add(pos, false);
            objectCount.Add(pos, 0);
        }
    }
}