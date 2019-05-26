using System;
using System.Collections;
using UnityEngine;
public class Unit : MonoBehaviour {
    public Tile tile { get; protected set; }
    private Board board;

    [SerializeField] EnergyValues energyValues;
    [Serializable]
    public class Energy {
        public float current;
        public float max;
        public float rate;
        public float barSize;

        public void Tick () {
            Regenerate ();
        }

        public int CurrentBars () {
            return (int) Mathf.Floor (current / barSize);
        }

        public bool SpendBars (uint cost) {
            if (CurrentBars () < cost) {
                return false;
            }
            current -= barSize * cost;
            return true;
        }

        private void Regenerate () {
            Add (rate * Time.deltaTime);
        }

        private void Add (float amount) {
            current += amount;
            // Clamp current to max
            if (current >= max) {
                current = max;
            }
        }

        public Energy (float _current, float _max, float _rate, float _barSize) {
            this.current = _current;
            this.max = _max;
            this.rate = _rate;
            this.barSize = _barSize;
        }
        public Energy (EnergyValues energyValues) {
            this.current = energyValues.current;
            this.max = energyValues.max;
            this.rate = energyValues.rate;
            this.barSize = energyValues.barSize;
        }
    };

    private Energy energy;

    private void Awake () {
        board = FindObjectOfType<Board> ();

        energy = energyValues != null ? new Energy (energyValues) : new Energy (30, 30, 6f, 10);
    }
    public void Place (Tile target) {
        // Make sure old tile location is not still pointing to this unit
        if (tile != null && tile.content == gameObject) {
            tile.content = null;
        }

        // Link unit and tile references
        tile = target;

        if (target != null) {
            if (target.content != null) {
                Destroy (target.content);
            }
            target.content = gameObject;
        }

    }
    public void Match () {
        transform.localPosition = tile.center;
    }

    // need to perform based on controller
    protected virtual IAction GetAction () {
        return new NoAction ();
    }

    public void MoveToPoint (Point p) {
        if (board.GetTile (p)) {
            this.Place (board.GetTile (p));
            this.Match ();
        }
    }

    private void Update () {
        energy.Tick ();

        IAction action = GetAction ();

        if (energy.SpendBars (action.GetCost ())) {
            action.Perform (this.gameObject);
        }
    }
}