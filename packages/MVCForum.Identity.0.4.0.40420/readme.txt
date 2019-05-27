
Thank you for installing ASP.NET Identity for MVC Forum!

Please take a look at the readme.txt for a look at what needs to be done to install MVC Forum. This readme file concentrates on what needs to be done to get Identity running with MVC Forum.

A few things that needs to be done before you're ready to use MVC Forum:

If Ninject has created an App_Start folder containing a class file (NinjectWebCommon.cs), please delete this file!

You need to configure MVC Forum to run, so in the Application_Start method, at the very top, you should add this piece of code:

		protected void Application_Start() {
			mvcForum.Web.ApplicationConfiguration.Initialize();

			// Whatever other code you need
		}

MVC Forum runs in a couple of Mvc areas, so we need to tell Mvc routing where to locate the controllers so you need to add the namespaces argument to the default route.
Edit the RouteConfig.RegisterRoutes method:

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
				namespaces: new String[] { "yourmvcappnamespace.Controllers" }
			);

Now it's time to edit the web.config file. Insert a connection string that matches your needs, with an account that can create new databases. Please make sure the connection string has an "providerName" attribute, and that the value is "System.Data.SqlClient".

Also in the web.config file, you'll need to change a few things for MVC Forum to run with ASP.NET Identity:

Locate these lines, and change the class paths and namespaces to match your project:

		<databaseBuilder type="Mvc5MvcForum.MvcForumIdentity.IdentityBuilder, Mvc5MvcForum" />
		<membershipServiceComponent type="Mvc5MvcForum.MvcForumIdentity.MembershipService, Mvc5MvcForum" />
		<formsAuthenticationComponent type="Mvc5MvcForum.MvcForumIdentity.AuthenticationService, Mvc5MvcForum" />
		<userProviderComponent type="Mvc5MvcForum.MvcForumIdentity.IdentityUserProvider, Mvc5MvcForum" />

Lastly, MVC Forum has it's own log on and log off actions, so go edit the app_start/Startup.Auth.cs.
You need to change this section:

            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/forum/account/logon")
            });

ASP.NET Identity for MVC Forum does not yet support third party login providers (we will, soon!).
