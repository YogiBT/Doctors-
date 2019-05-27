using ApplicationBoilerplate.DataProvider;
using ApplicationBoilerplate.DependencyInjection;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using $rootnamespace$.Models;
using mvcForum.DataProvider.EntityFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;

namespace $rootnamespace$.MvcForumIdentity {

	public class IdentityBuilder : IDependencyBuilder {

		public virtual void Configure(IDependencyContainer container) {
			Database.SetInitializer<IdentityDbContext>(new CreateDatabaseIfNotExists<IdentityDbContext>());

			container.RegisterPerRequest<IContext, Context>();
			container.RegisterPerRequest<DbContext, IdentityDbContext>(new Dictionary<String, Object> { { "nameOrConnectionString", ConfigurationManager.ConnectionStrings["mvcForum.DataProvider.MainDB"].ConnectionString } });

			container.RegisterGeneric(typeof(IRepository<>), typeof(Repository<>));

			// TODO: Do this in some other way!!
			new SpecificRepositoryConfiguration().Configure(container);

			//container.RegisterGenericPerRequest(typeof(IUserStore<ApplicationUser>), typeof(UserStore<ApplicationUser>));
			//container.RegisterGenericPerRequest(typeof(UserManager<ApplicationUser>), typeof(UserManager<ApplicationUser>));
			//container.RegisterGenericPerRequest(typeof(RoleManager<IdentityRole>), typeof(RoleManager<IdentityRole>));
		}

		public void ValidateRequirements(IList<ApplicationRequirement> feedback) { }
	}
}