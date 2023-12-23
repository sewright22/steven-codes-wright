using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using PlayoffPool.MVC.Extensions;

namespace PlayoffPool.MVC.Areas.Admin.Models
{
    public class UserModel
    {
        [Required]
        public string Id { get; set; } = "New_User";
        public string? RoleId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

        public string? FullName
        {
            get
            {
                if (this.FirstName.HasValue() == false && this.LastName.HasValue() == false)
                {
                    return "Unknown";
                }

                return $"{this.FirstName} {this.LastName}".Trim();
            }
        }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public string? Role { get; set; }

        [Display(Name = "Reset Password")]
        public bool ShouldResetPassword { get; set; }
        public List<SelectListItem> Roles { get; set; } = new List<SelectListItem>();
    }
}
