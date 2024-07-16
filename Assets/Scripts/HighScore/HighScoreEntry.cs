
namespace Dennis.HighScore
{
    [System.Serializable]
    public class HighScoreEntry
    {
        private string name;
        public string Name { get { return name; } }
        private int score;
        public int Score { get { return score; } }

        public HighScoreEntry(string name, int score)
        {
            this.name = name;
            this.score = score;
        }
    }
}