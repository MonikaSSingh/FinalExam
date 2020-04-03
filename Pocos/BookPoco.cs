using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pocos
{
    public class BookPoco:IPoco
    {
        [Key]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public int ISBN { get; set; }
        public string AuthorName { get; set; }
        public DateTime PublishedDate { get; set; }

        public virtual UserPoco User { get; set; }
    }
}
