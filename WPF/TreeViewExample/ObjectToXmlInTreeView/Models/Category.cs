using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectToXmlInTreeView.Models
{
    public class Category
    {
        public string CategoryName { get; set; }
        public List<SubCategory> SubCategories { get; set; }

        public Category()
        {

        }

        public Category(bool bCreateAndInit)
        {
            if (bCreateAndInit)
            {
                CategoryName = "Produce";
                SubCategories = new List<SubCategory>()
                {
                    new SubCategory()
                    {
                        SubCategoryName = "Vegatables",
                        Items = new List<Item>()
                        {
                            new Item()
                            {
                                ItemName = "Tomatoe"
                            },
                            new Item()
                            {
                                ItemName = "Cucumber"
                            }
                        }
                    },
                    new SubCategory()
                    {
                        SubCategoryName = "Fruits",
                        Items = new List<Item>()
                        {
                            new Item()
                            {
                                ItemName = "Banana"
                            },
                            new Item()
                            {
                                ItemName = "Apple"
                            },
                            new Item()
                            {
                                ItemName = "Orange"
                            }
                        }
                    },
                    new SubCategory()
                    {
                        SubCategoryName = "Meat",
                        Items = new List<Item>()
                        {
                            new Item()
                            {
                                ItemName = "Beef"
                            },
                            new Item()
                            {
                                ItemName = "Pork"
                            }
                        }
                    }
                };
            }
        }
    }
}
