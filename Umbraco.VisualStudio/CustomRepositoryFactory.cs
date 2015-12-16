using Umbraco.Core;
using Umbraco.Core.Configuration.UmbracoSettings;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.Repositories;
using Umbraco.Core.Persistence.SqlSyntax;
using Umbraco.Core.Persistence.UnitOfWork;

namespace Naive.Cache
{
    public class CustomRepositoryFactory : RepositoryFactory
    {
        private readonly CacheHelper cacheHelper;
        private readonly ILogger logger;
        private readonly ISqlSyntaxProvider sqlSyntax;

        public CustomRepositoryFactory(CacheHelper cacheHelper, ILogger logger, ISqlSyntaxProvider sqlSyntax, IUmbracoSettingsSection settings) : base(cacheHelper, logger, sqlSyntax, settings)
        {
            this.cacheHelper = cacheHelper;
            this.logger = logger;
            this.sqlSyntax = sqlSyntax;
        }

        public override IPublicAccessRepository CreatePublicAccessRepository(IDatabaseUnitOfWork uow)
        {
            return new PublicAccessRepository(uow, cacheHelper, logger, sqlSyntax);
        }

        public override ILanguageRepository CreateLanguageRepository(IDatabaseUnitOfWork uow)
        {
            return new LanguageRepository(uow, cacheHelper, logger, sqlSyntax);
        }

        public override IDomainRepository CreateDomainRepository(IDatabaseUnitOfWork uow)
        {
            return new DomainRepository(uow, cacheHelper, logger, sqlSyntax);
        }
    }

 
}