using UniRx;

namespace Utils
{
    /// <summary>
    /// Base class for game events
    /// </summary>
    public class WEvent { }

    /// <summary>
    /// An simple message manager, game events to be tracked can be sent through this class while reacting classes can track them by subscribing
    /// </summary>
    public static class MessageManager
    {
        /// <summary>
        /// Sends an event
        /// </summary>
        /// <param name="ev">Event to be sent</param>
        public static void Send<T>(T ev) where T : WEvent
        {
            MessageBroker.Default.Publish(ev);
        }

        /// <summary>
        /// Receives an event
        /// </summary>
        /// <typeparam name="T">Event type to react to</typeparam>
        /// <returns>A subscribable interface to subscribe to</returns>
        public static IObservable<T> Receive<T>() where T : WEvent
        {
            return MessageBroker.Default.Receive<T>();
        }
    }
}