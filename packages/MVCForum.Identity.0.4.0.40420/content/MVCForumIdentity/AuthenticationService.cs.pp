using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using $rootnamespace$.Models;
using mvcForum.Core.Interfaces.Services;
using System;
using System.Data.Entity;
using System.Net;
using System.Web;

namespace $rootnamespace$.MvcForumIdentity {

	public class AuthenticationService : IAuthenticationService {
		private readonly DbContext context;

		public AuthenticationService(DbContext context) {
			this.context = context;
		}

		public void SignIn(IAccount account, Boolean createPersistentCookie) {
			UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.context));
			ApplicationUser user = manager.FindByName(account.AccountName);

			this.AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
			var identity = manager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
			this.AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = createPersistentCookie }, identity);
		}

		public void SignOut() {
			this.AuthenticationManager.SignOut();
		}

		private IAuthenticationManager AuthenticationManager {
			get {
				return HttpContext.Current.GetOwinContext().Authentication;
			}
		}
	}
}