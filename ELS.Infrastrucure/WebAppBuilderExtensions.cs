using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ELS.Infrastrucure
{
    public static class WebAppBuilderExtensions
    {
        public static void AddMyServices(this IServiceCollection services, Type serviceType)
        {
            Assembly? serviceAssembly = Assembly.GetAssembly(serviceType);
            if (serviceAssembly == null)
            {
                throw new InvalidOperationException("Invalid service");
            }
            Type[] serivcesType = serviceAssembly.GetTypes().Where(x => x.Name.EndsWith("Service") && !x.IsInterface).ToArray();
            foreach (Type type in serivcesType)
            {
                Type? getTypeInterfaces = type.GetInterface($"I{type.Name}");
                if (getTypeInterfaces == null)
                {
                    throw new InvalidOperationException("No interface provided for the service.");
                }

                services.AddScoped(getTypeInterfaces, type);
            }
        }
    }
}
