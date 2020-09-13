using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace Alopeyk.Net.Dto.GetOrderDetails.Remote
{
    internal class GetOrderDetailsScoreResponseRemoteDto
    {
        public int score { get; set; }
        public Dictionary<string, double> score_detail { get; set; }
    }
}