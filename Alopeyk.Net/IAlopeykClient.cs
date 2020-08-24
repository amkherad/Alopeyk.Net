using System.Threading;
using System.Threading.Tasks;

namespace Alopeyk.Net
{
    public interface IAlopeykClient
    {
        Task<object> GetLocation(
            double latitude,
            double longitude,
            CancellationToken cancellationToken
        );


        Task<object> GetPrice(
            string appId,
            int orderId,
            string transportType,
            string city,
            object[] addresses,
            bool hasReturn,
            bool cached,
            CancellationToken cancellationToken
        );

        Task<object> TakeAlopeyk(
            string appId,
            int orderId,
            string transportType,
            string city,
            object[] addresses,
            bool hasReturn,
            bool cashed,
            CancellationToken cancellationToken
        );

        Task<object> GetAlopeykOrderStatus(
            long transferId,
            CancellationToken cancellationToken
        );


        Task<object> CancelAlopeyk(
            long transferId,
            CancellationToken cancellationToken
        );
    }
}