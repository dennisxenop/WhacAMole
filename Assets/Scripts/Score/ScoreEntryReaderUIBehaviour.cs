using Dennis.Score;
using Dennis.UI;
using Dennis.Variables;

namespace Dennis.Score
{
    public class ScoreEntryReaderUIBehaviour : VariableUIReader<ScoreVariable, ScoreEntry>
    {
        public override void CurrentVariableChanged()
        {
            if (variableToRead.Value.Score < 0) return;
            textObject.text = variableToRead.Value.Score.ToString();
        }
    }
}