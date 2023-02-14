﻿using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace SetupShop.Models
{
    public class Setup
    {
        // Properties
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        public string Author { get; set; }

        [Required(ErrorMessage = "Please upload a file")]
        public byte[] File { get; set; }

        [Required(ErrorMessage = "Please enter a value")]
        public string Car { get; set; }

        [Required(ErrorMessage = "Please enter a value")]
        public string Track { get; set; }

        [Required(ErrorMessage = "Please enter a value")]
        public string Season { get; set; }

        [Required(ErrorMessage = "Please enter a value")]
        public string Week { get; set; }

        [Required(ErrorMessage = "Please enter a value")]
        public string Series { get; set; }

        [Required(ErrorMessage = "Please enter a value")]
        public string VideoUrl { get; set; }

        [Required]
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; }

        public string Image { get; set; }
    }
}
