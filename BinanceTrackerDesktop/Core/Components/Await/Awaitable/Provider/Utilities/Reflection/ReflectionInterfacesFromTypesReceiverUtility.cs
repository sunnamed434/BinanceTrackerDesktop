using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Component;
using System.Reflection;
using System.Runtime.Serialization;

namespace BinanceTrackerDesktop.Core.Components.Await.Awaitable.Provider.Utilities.Reflection
{
    public sealed class ReflectionInterfacesFromTypesReceiverUtility
    {
        /// <summary>
        /// Receives all interfaces from given <paramref name="types"/>
        /// </summary>
        /// <typeparam name="TInterfaceTarget"></typeparam>
        /// <param name="types"></param>
        /// <returns>Received interfaces.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static IEnumerable<TInterfaceTarget> GetInterfacesFromTypes<TInterfaceTarget>(Type[] types) 
        {
            if (types == null)
                throw new ArgumentNullException(nameof(types));

            if (types.Any() == false)
                throw new InvalidOperationException();

            string interfaceTargetName = typeof(TInterfaceTarget).Name;
            if (typeof(TInterfaceTarget).IsInterface == false)
                throw new ArgumentException(interfaceTargetName);

            foreach (Type type in types)
            {
                if (type.GetInterface(interfaceTargetName) != null)
                {
                    if (type.GetInterface(nameof(IAwaitableSingletonObject)) != null)
                    {
                        IAwaitableSingletonObject singletonObject = (IAwaitableSingletonObject)FormatterServices.GetUninitializedObject(type);
                        yield return (TInterfaceTarget)singletonObject.Instance;
                    }
                    else
                    {
                        TInterfaceTarget target = default;
                        if ((target = (TInterfaceTarget)Activator.CreateInstance(type)) != null)
                        {
                            yield return target;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Receives all interfaces by calling method <see cref="GetInterfacesFromTypes{TInterfaceTarget}(Type[])"/>, from giving types from current executing <see cref="Assembly"/>
        /// </summary>
        /// <typeparam name="TInterfaceTarget"></typeparam>
        /// <returns>Received interfaces.</returns>
        public static IEnumerable<TInterfaceTarget> GetInterfacesFromAssemblyTypes<TInterfaceTarget>()
        {
            return GetInterfacesFromTypes<TInterfaceTarget>(Assembly.GetExecutingAssembly().GetTypes());
        }
    }
}
