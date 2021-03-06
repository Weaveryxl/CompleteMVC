﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CompleteMVC.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}