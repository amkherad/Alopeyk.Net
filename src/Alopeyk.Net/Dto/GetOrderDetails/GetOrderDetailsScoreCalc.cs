using System.Collections.Generic;

namespace Alopeyk.Net.Dto.GetOrderDetails
{
    public class GetOrderDetailsScoreCalc
    {
        public int Score { get; set; }
        public Dictionary<string, double> ScoreDetail { get; set; }
    }
}