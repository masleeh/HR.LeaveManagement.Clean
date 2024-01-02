using HR.LeaveManagement.Identity.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Identity.Configuration {
	public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser> {
		public void Configure(EntityTypeBuilder<ApplicationUser> builder) {
			var hasher = new PasswordHasher<ApplicationUser>();
			builder.HasData(
				new ApplicationUser {
					Id = "8e445865-a24d-4543-a6c6-9443d048cdb9",
					Email = "masleeh@gmail.asd",
					NormalizedEmail = "MASLEEH@GMAIL.ASD",
					FirstName = "Masle",
					LastName = "Eh",
					UserName = "masleeh",
					NormalizedUserName = "MASLEEH",
					PasswordHash = hasher.HashPassword(null, "P@ssw0rd!"),
					EmailConfirmed = true
				},				
				new ApplicationUser {
					Id = "9e224968-33e4-4652-b7b7-8574d048cdb9",
					Email = "user@gmail.com",
					NormalizedEmail = "USER@GMAIL.COM",
					FirstName = "Benjamin",
					LastName = "Fuckon",
					UserName = "benjfukc",
					NormalizedUserName = "BENJFUKC",
					PasswordHash = hasher.HashPassword(null, "P@ssw0rd!"),
					EmailConfirmed = true
				}
			);
		}
	}
}
