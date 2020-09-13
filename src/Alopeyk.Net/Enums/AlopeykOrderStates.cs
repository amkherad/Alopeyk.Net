namespace Alopeyk.Net.Enums
{
    public enum AlopeykOrderStates
    {
        /// <summary>
        /// Unknown order state.
        /// </summary>
        Unknown = 0,
        
        /// <summary>
        /// The order has been created and is ready to be dispatched to the nearest courier available.
        /// </summary>
        New = 1,
        
        /// <summary>
        /// The dispatcher machine is currently looking for near-by available couriers and is waiting for them to accept the order.
        /// </summary>
        Searching = 2,
        
        /// <summary>
        /// The order has been cancelled. if this event takes place by the customer it has to be before the picking status
        /// otherwise it means that the support team has cancelled the order.
        /// </summary>
        Cancelled = 3,
        
        /// <summary>
        /// No available courier was found for the order or no courier has responded or accepted your request
        /// </summary>
        Expired = 4,
        
        /// <summary>
        /// One of our couriers has accepted the order.
        /// </summary>
        Accepted = 5,
        
        /// <summary>
        /// The courier has arrived at the source of the order which is in fact the first address (the origin).
        /// </summary>
        Picking = 6,
        
        /// <summary>
        /// The courier has successfuly handled the first address and is now delivering the package(s).
        /// </summary>
        Delivering = 7,
        
        /// <summary>
        /// The courier successfully dropped all packages at their designated addresses i.e.
        /// the courier has handled those addresses.
        /// </summary>
        Delivered = 8,
        
        /// <summary>
        /// This status is not required. You can finish the order and rate our courier in which case the rate and comment
        /// attributes will be filled by you while updating the order to this status.
        /// </summary>
        Finished = 9,
        
        /// <summary>
        /// This status is specific to the scheduled orders. Once the scheduled_at timestamp is reached, the order will
        /// be dispatched automatically and its status will be updated to new. From this stage the orders will follow
        /// the same routine listed above.
        /// </summary>
        Scheduled = 10
    }
}