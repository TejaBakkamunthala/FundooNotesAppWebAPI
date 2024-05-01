using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//2) create a table product(productid, brand, productname) */

namespace RepositoryLayer.Entity
{
    public class ProductEntity
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ProductId {  get; set; }

        public string Brand { get; set; }

        public string ProductName { get; set; }

    }
}
