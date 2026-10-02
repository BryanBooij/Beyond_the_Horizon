namespace Assets.Scripts.Game
{
    public static class ScoreHelper
    {
        public static void AddPoints(float amount)
        {
            if (EndlessScoreManager.Instance != null)
                EndlessScoreManager.Instance.AddPoints(amount);
            else if (ScoreManager.Instance != null)
                ScoreManager.Instance.AddPoints(amount);
        }

        public static void HighScoreUpdate()
        {
            if (EndlessScoreManager.Instance != null)
                EndlessScoreManager.Instance.HighScoreUpdate();
            else if (ScoreManager.Instance != null)
                ScoreManager.Instance.HighScoreUpdate();
        }
    }
}