using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Customer
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        [Required]
        [StringLength(255)]
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private bool _isSuscribedToNewsletter;

        public bool IsSuscribedToNewsletter
        {
            get { return _isSuscribedToNewsletter; }
            set { _isSuscribedToNewsletter = value; }
        }

        public MembershipType MembershipType { get; set; }
        public byte MembershipTypeId { get; set; }
        
    }
}