﻿using static System.Reflection.Metadata.BlobBuilder;
using System.ComponentModel.DataAnnotations;
using Humanizer.Localisation;

namespace ptoba_svoego_vhoda_reg_2.Models
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        
        public string FirstName { get; set; }

       
        public string LastName { get; set; }

        

        public string PhoneNumber { get; set; }


        public string Email { get; set; }

        public string Password{ get; set; }
        public ICollection<Bron> Brons { get; set; } = new List<Bron>();


        public User() { 
        }
    }
}
