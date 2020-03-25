﻿using Autofac;
using JetBrains.Annotations;
using Lykke.Common.MsSql;
using Lykke.Service.CustomerManagement.MsSqlRepositories.Contexts;
using Lykke.Service.CustomerManagement.MsSqlRepositories.Repositories;
using Lykke.Service.CustomerManagement.Domain.Repositories;
using Lykke.Service.CustomerManagement.Settings;
using Lykke.SettingsReader;

namespace Lykke.Service.CustomerManagement.Modules
{
    [UsedImplicitly]
    public class DataModule : Module
    {
        private readonly CustomerManagementSettings _settings;

        public DataModule(IReloadingManager<AppSettings> appSettings)
        {
            _settings = appSettings.CurrentValue.CustomerManagementService;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterMsSql(
                _settings.Db.DataConnString,
                connString => new CmContext(connString, false),
                dbConn => new CmContext(dbConn));

            builder.RegisterType<EmailVerificationCodeRepository>()
                .WithParameter(TypedParameter.From(_settings.VerificationEmailExpirePeriod))
                .As<IEmailVerificationCodeRepository>()
                .SingleInstance();

            builder.RegisterType<CustomerFlagsRepository>()
                .As<ICustomerFlagsRepository>()
                .SingleInstance();

            builder.RegisterType<CustomersRegistrationReferralDataRepository>()
                .As<ICustomersRegistrationReferralDataRepository>()
                .SingleInstance();

            builder.RegisterType<PhoneVerificationCodeRepository>()
                .As<IPhoneVerificationCodeRepository>()
                .SingleInstance();
        }
    }
}
