using Apps.MVCApp.Application.Hendlers.Clients;
using Apps.MVCApp.Application.Hendlers.Requests;
using Apps.MVCApp.Application.Hendlers.Users;
using MassTransit.ExtensionsDependencyInjectionIntegration;

namespace Apps.MVCApp.DI
{
    public static class DIRegisterExtensions
    {
        public static void MediatorHandlersRegister(this IServiceCollectionMediatorConfigurator config)
        {
            //requestsConsumers registrations
            config.AddConsumer<GetRequestsListConsumer>();

            config.AddConsumer<GetRequestByIdConsumer>();

            config.AddConsumer<UpdateRequestConsumer>();

            config.AddConsumer<DeleteRequestConsumer>();

            config.AddConsumer<CreateRequestConsumer>();

            //usersConsumers registrations
            config.AddConsumer<GetUsersListConsumer>();

            config.AddConsumer<GetUserByIdConsumer>();

            config.AddConsumer<UpdateUserConsumer>();

            config.AddConsumer<CreateUserConsumer>();

            config.AddConsumer<DeleteUserConsumer>();

            //clientsConsumers registrations
            config.AddConsumer<GetClientsListConsumer>();

            config.AddConsumer<GetClientByIdConsumer>();

            config.AddConsumer<CreateClientConsumer>();

            config.AddConsumer<DeleteClientConsumer>();

            config.AddConsumer<UpdateClientConsumer>();
        }
    }
}
