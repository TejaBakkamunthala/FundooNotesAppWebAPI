using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Entity
{
    public class CustomerEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CustomerId {  get; set; }
        public string CustomerName {  get; set; }

        public string CustomerAddress {  get; set; }

        public string CustomerCity { get; set; }

        public string CustomerEmail {  get; set; }

        public string CustomerPassword {  get; set; }

        public string CustomerPhoneNumber    { get; set; }
    }
}
