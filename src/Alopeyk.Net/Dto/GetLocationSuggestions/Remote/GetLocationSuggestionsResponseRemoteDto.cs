namespace Alopeyk.Net.Dto.GetLocationSuggestions.Remote
// ReSharper disable InconsistentNaming
{
    internal class GetLocationSuggestionsResponseRemoteDto
    {
        public string title { get; set; }

        public string region { get; set; }

        public double lat { get; set; }

        public double lng { get; set; }

        public string district { get; set; }

        public string city { get; set; }

        public string city_fa { get; set; }
    }
}