using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Pocos
{
    public class UserPoco:IPoco
    {
        [Key]
        public int UserId { get; set; }
        public string FullName { get; set; }
        public DateTime DOB { get; set; }

        public virtual ICollection<BookPoco> Books { get; set; }
    }
}
