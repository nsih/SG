// 예시
// 타일의 정보를 관리하는 클래스
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    public Dictionary<Vector3Int, bool> objectOnTile; // 오브젝트가 타일 위에 있는가?에 대한 정보
    public Dictionary<Vector3Int, int> objectCount;// 타일 위에 있는 오브젝트 개수에 대한 정보
    public Tilemap tilemap; // 타일맵 컴포넌트

    private void Start()
    {
        // 타일맵 내의 셀 좌표들에 대해서 타일이 있다면 딕셔너리에 초기 정보를 추가한다.
        foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
        {
            // 해당 좌표에 타일이 없으면 넘어간다.
            if (!tilemap.HasTile(pos)) continue;
            // 해당 좌표의 타일을 얻는다.
            var tile = tilemap.GetTile<TileBase>(pos);
            // 정보 초기화
            objectOnTile.Add(pos, false);
            objectCount.Add(pos, 0);
        }
    }
}