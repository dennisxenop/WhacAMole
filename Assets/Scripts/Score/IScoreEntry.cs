namespace Dennis.Score
{
    public interface IScoreEntry
    {
        public void HighlightScore();

        public void Setup(string name, int score);
    }
}