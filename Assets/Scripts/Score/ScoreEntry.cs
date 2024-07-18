
using System;
using UnityEngine;

namespace Dennis.Score
{
    [System.Serializable]
    public class ScoreEntry : ICloneable
    {
        [SerializeField]
        private string name;
        public string Name { get { return name; } }
        [SerializeField]
        private int score;
        public int Score { get { return score; } }

        public ScoreEntry() : this("",0){}
        public ScoreEntry(string name, int score)
        {
            this.name = name;
            this.score = score;
        }

        public object Clone()
        {
            return new ScoreEntry(name, score);
        }
    }

}