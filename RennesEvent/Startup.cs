using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using RennesEvent.Models;

[assembly: OwinStartupAttribute(typeof(RennesEvent.Startup))]
namespace RennesEvent
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            using (var context = new ApplicationDbContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                    if (!roleManager.RoleExists(RolesNames.Administrateur))
                    {
                        roleManager.Create(new IdentityRole(RolesNames.Administrateur));
                        var admin = new ApplicationUser
                        {
                            UserName = "admin@email.com",
                            Email = "admin@email.com"
                        };

                        var result = userManager.Create(admin, "Pa$$w0rd");
                        if (result.Succeeded)
                        {
                            userManager.AddToRole(admin.Id, RolesNames.Administrateur);

                            if (!roleManager.RoleExists(RolesNames.Moderateur))
                            {
                                roleManager.Create(new IdentityRole(RolesNames.Moderateur));
                            }

                            transaction.Commit();
                        }
                    }
                }
            }
        }
    }
}
