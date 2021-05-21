using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTracking.DAL;
using StockTracking.DAL.DTO;

namespace StockTracking.DAL.DAO
{
    class EmployeeDAO : StockContext, IDAO<EMPLOYEE, EmployeeDetailDTO>
    {
        public bool Delete(EMPLOYEE entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBack(int ID)
        {
            throw new NotImplementedException();
        }

        public bool Insert(EMPLOYEE entity)
        {
            try
            {
                db.EMPLOYEE.Add(entity);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EmployeeDetailDTO> Select()
        {
            try
            {
                List<EmployeeDetailDTO> employee = new List<EmployeeDetailDTO>();
                var list = (from e in db.EMPLOYEE
                            join p in db.PERMISSIONS on e.Permission equals p.ID
                            select new
                            {
                                id = e.ID,
                                userno = e.UserNumber,
                                name = e.Name,
                                surname = e.Surname,
                                pass = e.Password,
                                permission = p.ID
                            }
                                ).OrderBy(x => x.userno).ToList();
                foreach(var item in list)
                {
                    EmployeeDetailDTO dto = new EmployeeDetailDTO();
                    dto.ID = item.id;
                    dto.Name = item.name;
                    dto.Surname = item.surname;
                    dto.Password = item.pass;
                    dto.Permission = item.permission;
                    dto.UserNumer = item.userno;
                    employee.Add(dto);
                }
                return employee;
            }catch(Exception ex)
            {
                throw ex;
            }
        }

        internal List<EmployeeDetailDTO> Get(int v, string text)
        {
            try
            {
                List<EmployeeDetailDTO> emp = new List<EmployeeDetailDTO>();
                var list = (from e in db.EMPLOYEE
                            where e.UserNumber == v && e.Password == text
                            select new
                            {
                                ID = e.ID,
                                user = e.UserNumber,
                                name = e.Name,
                                surname = e.Surname,
                                pass = e.Password,
                                permission = e.Permission
                            }).ToList();
                foreach (var item in list)
                {
                    EmployeeDetailDTO dto = new EmployeeDetailDTO();
                    dto.Name = item.name;
                    dto.Surname = item.surname;
                    dto.ID = item.ID;
                    dto.Permission = item.permission;
                    dto.UserNumer = item.user;
                    dto.Password = item.pass;
                    emp.Add(dto);
                }
                return emp;
            }catch(Exception ex)
            {
                throw ex;
            }
        }


        public bool Update(EMPLOYEE entity)
        {
            try
            {
                EMPLOYEE employee = db.EMPLOYEE.First(x => x.UserNumber == entity.UserNumber);
                employee.Permission = entity.Permission;
                db.SaveChanges();
                return true;
            }catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
