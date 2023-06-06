﻿using System.ComponentModel.DataAnnotations;

namespace CRM.Models
{
    public class Notes
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Iban { get; set; }
    }
}
