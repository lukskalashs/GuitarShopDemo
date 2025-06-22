﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using GuitarShop.Models;

namespace GuitarShop.Infrastructure
{
    [HtmlTargetElement("img", Attributes = "profile-user")]
    public class ProfileImageTagHelper : TagHelper
    {
        private UserManager<AppUser> _userManager;

        public ProfileImageTagHelper(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HtmlAttributeName("profile-user")]
        public string UserName { get; set; }

        public override async Task ProcessAsync(TagHelperContext context,
            TagHelperOutput output)
        {
            AppUser user = await _userManager.FindByNameAsync(UserName);

            if (user != null && user.AvatarImage != null)
            {
                string mimeType = "image/jpeg";
                string base64 = Convert.ToBase64String(user.AvatarImage);
                string filename = string.Format("data:{0};base64,{1}", mimeType, base64);
                output.Attributes.SetAttribute("src", $"{filename}");
                output.Attributes.SetAttribute("class", "img-thumbnail rounded-circle");
                output.Attributes.SetAttribute("style", "height:30px");
            }
            else
            {
                // Handle the case where AvatarImage is null, e.g., use a default image
            }

        }
    }
}
