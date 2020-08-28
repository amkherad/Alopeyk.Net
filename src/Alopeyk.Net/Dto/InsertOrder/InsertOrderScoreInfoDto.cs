using System.Collections.Generic;

namespace Alopeyk.Net.Dto.InsertOrder
{
    public class InsertOrderScoreInfoDto
    {
        public int Score { get; set; }
        public Dictionary<int, string> ScoreDetail { get; set; }
    }
}