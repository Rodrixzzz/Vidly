using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{
    public class Movie
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }

        }
        [Required]
        [StringLength(50)]
        private string _name;
        
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        [Required]
        public DateTime ReleaseDate { get; set; }
        [Required]
        public DateTime AddedDate { get; set; }
        [Required]
        public byte Stock { get; set; }
        [Required]
        public Genre Genre { get; set; }
        [Required]
        public byte GenreId { get; set; }
        
    }
}