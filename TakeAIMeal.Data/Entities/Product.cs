// <auto-generated>
// ReSharper disable All

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TakeAIMeal.Data
{
    // ****************************************************************************************************
    // This is not a commercial licence, therefore only a few tables/views/stored procedures are generated.
    // ****************************************************************************************************

    // Products
    public class Product
    {
        public int Id { get; set; } // Id (Primary key)
        public int CategoryId { get; set; } // CategoryId
        public string Name { get; set; } // Name (length: 30)

        // Reverse navigation

        /// <summary>
        /// Child UserProductsExclusions where [UserProductsExclusions].[ProductId] point to this entity (FK__UserProdu__Produ__06CD04F7)
        /// </summary>
        public ICollection<UserProductsExclusion> UserProductsExclusions { get; set; } // UserProductsExclusions.FK__UserProdu__Produ__06CD04F7

        // Foreign keys

        /// <summary>
        /// Parent ProductCategory pointed by [Products].([CategoryId]) (FK__Products__Category__7D439ABD)
        /// </summary>
        public ProductCategory ProductCategory { get; set; } // FK__Products__Category__7D439ABD

        public Product()
        {
            UserProductsExclusions = new List<UserProductsExclusion>();
        }
    }

}
// </auto-generated>
