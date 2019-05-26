using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    [Serializable]
    public class TileSearchState {
        public Tile prev;
        public int distance;

        public TileSearchState () {
            this.prev = null;
            this.distance = int.MaxValue;
        }
    }

    [SerializeField] GameObject tilePrefab;
    public Dictionary<Point, Tile> tiles = new Dictionary<Point, Tile> ();
    Point[] dirs = new Point[4] {
        new Point (0, 1),
        new Point (0, -1),
        new Point (1, 0),
        new Point (-1, 0)
    };

    public void Load (LevelData data) {
        for (int i = 0; i < data.tiles.Count; ++i) {
            GameObject instance = Instantiate (tilePrefab) as GameObject;
            Tile t = instance.GetComponent<Tile> ();
            t.Load (data.tiles[i]);
            tiles.Add (t.pos, t);
        }
    }

    public List<Tile> Search (Tile start, Func<TileSearchState, TileSearchState, bool> addTile) {
        List<Tile> retValue = new List<Tile> ();
        retValue.Add (start);

        // ClearSearch ();
        Dictionary<Tile, TileSearchState> searchState = new Dictionary<Tile, TileSearchState> ();
        foreach (KeyValuePair<Point, Tile> entry in this.tiles) {
            searchState.Add (entry.Value, new TileSearchState ());
        }

        Queue<Tile> checkNext = new Queue<Tile> ();
        Queue<Tile> checkNow = new Queue<Tile> ();
        // start.distance = 0;
        searchState[start].distance = 0;

        checkNow.Enqueue (start);
        while (checkNow.Count > 0) {
            Tile t = checkNow.Dequeue ();
            for (int i = 0; i < 4; ++i) {
                Tile next = GetTile (t.pos + dirs[i]);
                // if (next == null || next.distance <= t.distance + 1) {
                if (next == null || searchState[next].distance <= searchState[t].distance + 1) {
                    continue;
                }

                //if (addTile (t, next)) {
                if (addTile (searchState[t], searchState[next])) {
                    searchState[next].distance = searchState[t].distance + 1;
                    searchState[next].prev = t;
                    checkNext.Enqueue (next);
                    retValue.Add (next);
                }
            }
            if (checkNow.Count == 0)
                SwapReference (ref checkNow, ref checkNext);
        }
        return retValue;
    }

    // void ClearSearch () {
    //     foreach (Tile t in tiles.Values) {
    //         t.prev = null;
    //         t.distance = int.MaxValue;
    //     }
    // }

    public Tile GetTile (Point p) {
        return tiles.ContainsKey (p) ? tiles[p] : null;
    }

    void SwapReference (ref Queue<Tile> a, ref Queue<Tile> b) {
        Queue<Tile> temp = a;
        a = b;
        b = temp;
    }
}