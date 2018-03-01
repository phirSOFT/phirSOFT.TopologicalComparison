using System;

namespace phirSOFT.TopologicalComparison
{
    /// <summary>
    ///     This exception is thrown, when a tree is in an invalid state.
    /// </summary>
    public class InvalidTreeStateException : InvalidOperationException
    {
        public InvalidTreeStateException()
        {
        }

        public InvalidTreeStateException(string message) : base(message)
        {
        }

        public InvalidTreeStateException(string message, Exception innerException) : base(message, innerException)
        {
        }

    }
}
