using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Caching;
using Umbraco.Core;
using Umbraco.Core.Cache;
using Umbraco.Core.Configuration;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.UnitOfWork;
using Umbraco.Core.Publishing;
using Umbraco.Core.Services;
using Umbraco.Web;

namespace Naive.Cache
{
    public class NaiveCacheBootManager : WebBootManager
    {
        public NaiveCacheBootManager(UmbracoApplicationBase umbracoApplication) : base(umbracoApplication)
        {
        }

        protected override ServiceContext CreateServiceContext(DatabaseContext dbContext, IDatabaseFactory dbFactory)
        {
            //use a request based messaging factory
            var evtMsgs = new RequestLifespanMessagesFactory(new SingletonUmbracoContextAccessor());
            return new ServiceContext(
                new CustomRepositoryFactory(ApplicationCache, ProfilingLogger.Logger, dbContext.SqlSyntax, UmbracoConfig.For.UmbracoSettings()),
                new PetaPocoUnitOfWorkProvider(dbFactory),
                new FileUnitOfWorkProvider(),
                new PublishingStrategy(evtMsgs, ProfilingLogger.Logger),
                ApplicationCache,
                ProfilingLogger.Logger,
                evtMsgs);
            return base.CreateServiceContext(dbContext, dbFactory);
        }

        protected override CacheHelper CreateApplicationCache()
        {
            return base.CreateApplicationCache();

            return new CacheHelper(
                new NullCacheProvider(),
                new NullCacheProvider(),
                new NullCacheProvider()
                );
        }

    }

    public class NullCacheProvider : IRuntimeCacheProvider
    {
        public virtual void ClearAllCache()
        {
        }

        public virtual void ClearCacheItem(string key)
        {
        }

        public virtual void ClearCacheObjectTypes(string typeName)
        {
        }

        public virtual void ClearCacheObjectTypes<T>()
        {
        }

        public virtual void ClearCacheObjectTypes<T>(Func<string, T, bool> predicate)
        {
        }




        public virtual void ClearCacheByKeySearch(string keyStartsWith)
        {
        }

        public virtual void ClearCacheByKeyExpression(string regexString)
        {
        }

        public virtual IEnumerable<object> GetCacheItemsByKeySearch(string keyStartsWith)
        {
            return Enumerable.Empty<object>();
        }

        public IEnumerable<object> GetCacheItemsByKeyExpression(string regexString)
        {
            return Enumerable.Empty<object>();
        }

        public virtual object GetCacheItem(string cacheKey)
        {
            return default(object);
        }

        public virtual object GetCacheItem(string cacheKey, Func<object> getCacheItem)
        {
            return getCacheItem();
        }

        public object GetCacheItem(string cacheKey, Func<object> getCacheItem, TimeSpan? timeout, bool isSliding = false, CacheItemPriority priority = CacheItemPriority.Normal, CacheItemRemovedCallback removedCallback = null, string[] dependentFiles = null)
        {
            return getCacheItem();
        }

        public void InsertCacheItem(string cacheKey, Func<object> getCacheItem, TimeSpan? timeout = null, bool isSliding = false, CacheItemPriority priority = CacheItemPriority.Normal, CacheItemRemovedCallback removedCallback = null, string[] dependentFiles = null)
        {

        }
    }
}