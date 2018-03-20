using System;
using System.Collections.Generic;
using System.Text;
using Site.Models.Entities;

namespace Site.Models.DTO
{
    public class UserDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        //nav
        public virtual List<TemplateDto> Templates { get; set; }

        public virtual List<ProjectDto> Projects { get; set; }

        public virtual List<UserProjectDto> InvolvedProjects { get; set; } = new List<UserProjectDto>(); // projects where user is involved in
    }
}
