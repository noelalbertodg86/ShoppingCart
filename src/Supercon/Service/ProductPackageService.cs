using System;
using System.Linq;
using System.Collections.Generic;
using Supercon.Model;
using CustomizeException;

namespace Supercon.Service
{
    public class ProductPackageService
    {

        private List<ProductPackage> productsComboList;

        public ProductPackageService()
        {
            this.productsComboList = new List<ProductPackage>();
        }

        public void CreateCombo(ProductPackage combo)
        {
            this.productsComboList.Add(combo);
        }

        public void  AddProductToCombo(ProductPackage _combo, Product _product)
        {
            this.productsComboList.Where(p => p.code == _combo.code).FirstOrDefault().productsList.Add(_product);
        }

        public void RemoveProductFromCombo(ProductPackage _combo, Product _product)
        {
            this.productsComboList.Where(p => p.code == _combo.code).FirstOrDefault().productsList.Remove(_product);
        }

        public List<ProductPackage> GetAllComboProducts()
        {
            return this.productsComboList;
        }

        public ProductPackage GetCombo(ProductPackage _combo)
        {
            return this.productsComboList.Where(p => p.code == _combo.code).FirstOrDefault();
        }

        public ProductPackage GetCombo(string _comboCode)
        {
            return this.productsComboList.Where(p => p.code == _comboCode).FirstOrDefault();
        }

        public List<Product> GetProductsFromCombo(ProductPackage _combo)
        {
            return this.productsComboList.Where(p => p.code == _combo.code).First().productsList;
        }

        public void ComboDataValidation(ProductPackage combo)
        {
            if (string.IsNullOrEmpty(combo.code)) { throw new ProductComboValidationExceptions("The product combo code cannot be null or empty"); }
        }

        public void SetComboDiscount(ProductPackage productCombo, Discount discount)
        {
            this.productsComboList.Where(p => p.code == productCombo.code).First().discount = discount;
        }
        public void SetComboDiscount(string productComboCode, Discount discount)
        {
            this.productsComboList.Where(p => p.code == productComboCode).First().discount = discount;
        }
    }
}
