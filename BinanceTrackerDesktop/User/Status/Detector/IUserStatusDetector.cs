using BinanceTrackerDesktop.User.Status.API;

namespace BinanceTrackerDesktop.User.Status.Detector;

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