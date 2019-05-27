using ApplicationBoilerplate.DataProvider;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using $rootnamespace$.Models;
using mvcForum.Core;
using mvcForum.Core.Specifications;
using mvcForum.Web.Interfaces;
using System;
using System.Data.Entity;
using System.Web;

namespace $rootnamespace$.MvcForumIdentity {

	public class IdentityUserProvider : IWebUserProvider {
		private readonly UserManager<ApplicationUser> userManager;
		private readonly IRepository<ForumUser> userRepo;

		public IdentityUserProvider(IRepository<ForumUser> userRepo, DbContext context) {
			this.userRepo = userRepo;
			this.userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
		}

		protected ForumUser user;
		public ForumUser ActiveUser {
			get {
				if (this.Authenticated) {
					return user;
				}
				return null;
			}
		}

		protected Boolean checkedAuthenticated = false;
		protected Boolean authenticated = false;
		public Boolean Authenticated {
			get {
				if (!this.checkedAuthenticated) {
					if (HttpContext.Current != null && HttpContext.Current.User.Identity.IsAuthenticated) {
						ApplicationUser u = this.userManager.FindByName(HttpContext.Current.User.Identity.Name);
						this.authenticated = (u != null);
						if (this.authenticated) {
							try {
								user = this.userRepo.ReadOne(new ForumUserSpecifications.SpecificProviderUserKey(u.Id));
							}
							catch { }
							this.authenticated = (user != null);
						}
					}
					this.checkedAuthenticated = true;
				}
				return this.authenticated;
			}
		}
	}
}