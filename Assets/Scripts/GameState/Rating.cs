using System;
using System.Collections.Generic;

namespace GameState
{
    [Serializable]
    public class Rating
    {
        private readonly List<int> _ratingScores = new List<int>();
        private int _maxScore;

        public IReadOnlyCollection<int> RatingScores => _ratingScores;

        public void Add(int value)
        {
            if (_ratingScores.Count == 0)
            {
                _ratingScores.Add(value);
                _maxScore = value;
                return;
            }
            if (value > _maxScore)
            {
                _ratingScores.Insert(0, value);
                _maxScore = value;
            }
        }
    }
}