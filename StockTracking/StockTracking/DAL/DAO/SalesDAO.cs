using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTracking.DAL;
using StockTracking.DAL.DTO;

namespace StockTracking.DAL.DAO
{
    class SalesDAO : StockContext, IDAO<SALES, SalesDetailDTO>
    {
        public bool Delete(SALES entity)
        {
            try
            {
                if(entity.ID !=0)
                {
                    SALES sales = db.SALES.First(x => x.ID == entity.ID);
                    sales.isDeleted = true;
                    sales.DeletedDate = DateTime.Today;
                    db.SaveChanges();
                }
                else if (entity.ProductID != 0)
                {
                    List<SALES> sales = db.SALES.Where(x => x.ProductID == entity.ProductID).ToList();
                    foreach (var item in sales)
                    {
                        item.isDeleted = true;
                        item.DeletedDate = DateTime.Today;
                    }
                    db.SaveChanges();
                }
                else if(entity.CustomerID != 0)
                {
                    List<SALES> sales = db.SALES.Where(x => x.CustomerID == entity.CustomerID).ToList();
                    foreach(var item in sales)
                    {
                        item.isDeleted = true;
                        item.DeletedDate = DateTime.Today;
                    }
                    db.SaveChanges();
                }
                
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool GetBack(int ID)
        {
            try
            {
                SALES sales = db.SALES.First(x => x.ID == ID);
                sales.isDeleted = false;
                sales.DeletedDate = null;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(SALES entity)
        {
            try
            {
                db.SALES.Add(entity);
                db.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SalesDetailDTO> Select()
        {
            try
            {
                List<SalesDetailDTO> sales = new List<SalesDetailDTO>();
                var list = (from s in db.SALES.Where(x=>x.isDeleted == false)
                            join p in db.PRODUCT on s.ProductID equals p.ID
                            join c in db.CUSTOMER on s.CustomerID equals c.ID
                            join category in db.CATEGORY on s.CategoryID equals category.ID
                            select new
                            {
                                productname = p.ProductName,
                                customername = c.CustomerName,
                                categoryname = category.CategoryName,
                                productID = s.ProductID,
                                customerID = s.CustomerID,
                                salesID = s.ID,
                                categoryID = s.CategoryID,
                                salesprice = s.ProductSalesPrice,
                                salesamount = s.ProductSalesAmount,
                                salesdate = s.SalesDate,
                                categoryDeleted = category.isDeleted,
                                customerDeleted = c.isDeleted,
                                productDeleted = p.isDeleted,
                            }).OrderBy(x => x.salesdate).ToList();
                foreach(var item in list)
                {
                    SalesDetailDTO dto = new SalesDetailDTO();
                    dto.CategoryID = item.categoryID;
                    dto.ProductID = item.productID;
                    dto.ProductName = item.productname;
                    dto.CustomerID = item.customerID;
                    dto.CustomerName = item.customername;
                    dto.SalesAmount = item.salesamount;
                    dto.SalesDate = item.salesdate;
                    dto.SalesID = item.salesID;
                    dto.Price = item.salesprice;
                    dto.CategoryName = item.categoryname;
                    dto.isCategoryDeleted = item.categoryDeleted;
                    dto.isCustomerDeleted = item.customerDeleted;
                    dto.isProductDeleted = item.productDeleted;
                    sales.Add(dto);
                }
                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SalesDetailDTO> Select(bool isDeleted)
        {
            try
            {
                List<SalesDetailDTO> sales = new List<SalesDetailDTO>();
                var list = (from s in db.SALES.Where(x => x.isDeleted == isDeleted)
                            join p in db.PRODUCT on s.ProductID equals p.ID
                            join c in db.CUSTOMER on s.CustomerID equals c.ID
                            join category in db.CATEGORY on s.CategoryID equals category.ID
                            select new
                            {
                                productname = p.ProductName,
                                customername = c.CustomerName,
                                categoryname = category.CategoryName,
                                productID = s.ProductID,
                                customerID = s.CustomerID,
                                salesID = s.ID,
                                categoryID = s.CategoryID,
                                salesprice = s.ProductSalesPrice,
                                salesamount = s.ProductSalesAmount,
                                salesdate = s.SalesDate,
                            }).OrderBy(x => x.salesdate).ToList();
                foreach (var item in list)
                {
                    SalesDetailDTO dto = new SalesDetailDTO();
                    dto.CategoryID = item.categoryID;
                    dto.ProductID = item.productID;
                    dto.ProductName = item.productname;
                    dto.CustomerID = item.customerID;
                    dto.CustomerName = item.customername;
                    dto.SalesAmount = item.salesamount;
                    dto.SalesDate = item.salesdate;
                    dto.SalesID = item.salesID;
                    dto.Price = item.salesprice;
                    dto.CategoryName = item.categoryname;
                    sales.Add(dto);
                }
                return sales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Update(SALES entity)
        {
            try
            {
                SALES sales = db.SALES.First(x => x.ID == entity.ID);
                sales.ProductSalesAmount = entity.ProductSalesAmount;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
