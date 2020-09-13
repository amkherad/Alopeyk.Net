namespace Alopeyk.Net.Dto
// ReSharper disable InconsistentNaming
{
    internal class RemoteBaseResponseDto<T>
    {
        public string status { get; set; }
            
        public string message { get; set; }

        public T @object { get; set; }
    }
}