using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICar.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Idcar { get; set; }
        public DateTime? Date { get; set; }
    }
}
