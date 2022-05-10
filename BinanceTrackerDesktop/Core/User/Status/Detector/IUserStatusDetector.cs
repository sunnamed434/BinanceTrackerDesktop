using BinanceTrackerDesktop.Core.User.Control;

namespace BinanceTrackerDesktop.Core.User.Status.Detector
{
    /// <summary>
    /// User status detector.
    /// </summary>
    public interface IUserStatusDetector
    {
        /// <summary>
        /// Detect user status.
        /// </summary>
        /// <returns>User status.</returns>
        IUserStatus GetStatus();
    }
}