using System;

namespace PJL.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class ServiceAttribute : Attribute
    {
        public string InitializationMethodName { get; }

        /// <param name="initializationMethodName">
        /// The name of the method that will be used in order to initialize a new instance of the service.
        /// If string.IsNullOrWhiteSpace called on the argument returns true, the constructor will be used if the class is not
        /// a MonoBehaviour. If it is, no method will be called.
        /// </param>
        public ServiceAttribute(string initializationMethodName)
        {
            InitializationMethodName = initializationMethodName;
        }
    }
}