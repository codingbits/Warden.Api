﻿using System;
using System.Threading.Tasks;
using Rebus.Bus;
using Warden.Api.Core.Domain.Security;
using Warden.Api.Core.Domain.Wardens;
using Warden.Api.Core.Repositories;
using Warden.Api.Core.Types;
using Warden.Api.Infrastructure.DTO.WardenConfigurations;
using Warden.Bus.Commands;
using JsonConvert = Newtonsoft.Json.JsonConvert;

namespace Warden.Api.Infrastructure.Services
{
    public interface IWardenConfigurationService
    {
        Task CreateWardenAsync(object configuration);
        Task<Maybe<WardenConfigurationDto>> GetConfigurationAsync(Guid id, string token);
    }

    public class WardenConfigurationService : IWardenConfigurationService
    {
        private readonly IWardenConfigurationRepository _wardenConfigurationRepository;
        private readonly ISecuredRequestRepository _securedRequestRepository;
        private readonly IBus _bus;

        public WardenConfigurationService(IWardenConfigurationRepository wardenConfigurationRepository, 
            ISecuredRequestRepository securedRequestRepository,
            IBus bus)
        {
            _wardenConfigurationRepository = wardenConfigurationRepository;
            _securedRequestRepository = securedRequestRepository;
            _bus = bus;
        }

        //TODO: Implement this properly.
        public async Task CreateWardenAsync(object configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration), "Warden configuration can not be null.");

            var serializedConfiguration = JsonConvert.SerializeObject(configuration);
            var wardenConfiguration = new WardenConfiguration(serializedConfiguration);
            await _wardenConfigurationRepository.AddAsync(wardenConfiguration);
            var securedRequest = new SecuredRequest(SecuredResource.WardenConfiguration, wardenConfiguration.Id);
            await _securedRequestRepository.AddAsync(securedRequest);
            var userId = Guid.NewGuid().ToString();
            var token = securedRequest.Token.Token;
            var region = "EU";
            await _bus.Publish(new CreateWarden(userId, wardenConfiguration.Id.ToString(), token, region));
        }

        public async Task<Maybe<WardenConfigurationDto>> GetConfigurationAsync(Guid id, string token)
        {
            var configuration = await _wardenConfigurationRepository.GetAsync(id);
            if (configuration.HasNoValue)
                return new Maybe<WardenConfigurationDto>();

            var securedRequest = await _securedRequestRepository.GetByResourceIdAsync(id);
            if (securedRequest.HasValue)
            {
                securedRequest.Value.Consume(token);
                await _securedRequestRepository.UpdateAsync(securedRequest.Value);
            }

            var deserializedConfiguration = JsonConvert.DeserializeObject(configuration.Value.Configuration);

            return new WardenConfigurationDto
            {
                Configuration = deserializedConfiguration
            };
        }
    }
}