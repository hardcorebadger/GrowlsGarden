using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController Instance;

	public GameObject Pointer;
	public float ArmLength;
	public float ThrowSpeed;
	public int PaintRadius = 5;

	private Terrain _terrain;

	void Awake () {
		Instance = this;
		_terrain = FindObjectOfType<Terrain> ();
	}

	public GameObject GetTopParent(GameObject g) {
		if (g.transform.parent != null)
			return GetTopParent (g.transform.parent.gameObject);
		return g;
	}

	public void PaintRoad(Vector3 pos) {
		int x = Mathf.RoundToInt(((pos.x - _terrain.transform.position.x) / _terrain.terrainData.size.x) * _terrain.terrainData.heightmapWidth);
		int y = Mathf.RoundToInt(((pos.z - _terrain.transform.position.z) / _terrain.terrainData.size.z) * _terrain.terrainData.heightmapHeight);
		float[,,] splatmap = _terrain.terrainData.GetAlphamaps(x-PaintRadius,y-PaintRadius,PaintRadius*2,PaintRadius*2);
		int[,] details0 = _terrain.terrainData.GetDetailLayer (x - PaintRadius, y - PaintRadius, (PaintRadius * 2), (PaintRadius * 2), 0);
		int[,] details1 = _terrain.terrainData.GetDetailLayer (x - PaintRadius, y - PaintRadius, (PaintRadius * 2), (PaintRadius * 2), 1);
		for (int xi = 0; xi < splatmap.GetLength(0); xi++) {
			for (int yi = 0; yi < splatmap.GetLength(1); yi++) {
				int p =  Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(PaintRadius,2) - (Mathf.Pow (xi - PaintRadius, 2) + Mathf.Pow (yi - PaintRadius, 2))));
				if (p > PaintRadius/2f) {
					splatmap [xi, yi, 0] = 0;
					splatmap [xi, yi, 1] = 1;
					details0 [xi, yi] = 0;
					details1 [xi, yi] = 0;
				} else if (p >= 0) {
					splatmap [xi, yi, 0] = 0;
					splatmap [xi, yi, 1] = 1;
				}
			}
		}

		_terrain.terrainData.SetAlphamaps (x-PaintRadius,y-PaintRadius,splatmap);
		_terrain.terrainData.SetDetailLayer(x-PaintRadius,y-PaintRadius, 0, details0);
		_terrain.terrainData.SetDetailLayer(x-PaintRadius,y-PaintRadius, 1, details1);

	}
}
