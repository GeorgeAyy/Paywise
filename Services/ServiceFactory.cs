namespace MyMvcApp.Services
{
    public interface IServiceFactory
    {
        T CreateService<T>() where T : class;
    }

    public class ServiceFactory : IServiceFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T CreateService<T>() where T : class
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
