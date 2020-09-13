using System.Collections.Generic;
// ReSharper disable InconsistentNaming

namespace Alopeyk.Net.Dto.InsertOrder.Remote
{
    internal class InsertOrderScoreRemoteDto
    {
        public int score { get; set; }
        public Dictionary<string, double> score_detail { get; set; }
    }
}