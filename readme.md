This project contains simple naive versions of the Umbraco 7.3 repositories for Domains, Languages and PublicAccessEntries.  

In it's current state it's ment as a proof of performance problems in sites using those entities in the front-end.  
It is provided as-is, without any support, and with no guarantee what-so-ever if used in production. :)

To use it, you'll have to make a custom build of 

-   Umbraco.Core where `RepositoryFactory.CreateDomainRepository` is made virtual.
-   umbraco (Umbraco.Web.UI) where Umbraco.VisualStudio is added in an `InternalsVisibleTo` assembly attribute.

(These will be PR'd)

You'll also have to swap out the default boot manager, or make your own.
Replace the Global.asax @inherits class with your own Global class inheriting from `UmbracoApplication`

    using Umbraco.Core;
    using Umbraco.Web;

    namespace YourSite
    {
        public class Global : UmbracoApplication
        {
            protected override IBootManager GetBootManager()
            {
                return new NaiveCacheBootManager(this);
            }
        }
    }

The builds of Umbraco in the lib folder is modified versions of the 7.3.2 build.