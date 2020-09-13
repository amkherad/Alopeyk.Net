using System.Collections.Generic;

namespace Alopeyk.Net.Dto.InsertOrder
{
    public class InsertOrderScoreInfoDto
    {
        public int Score { get; set; }
        public Dictionary<string, double> ScoreDetail { get; set; }
    }
}