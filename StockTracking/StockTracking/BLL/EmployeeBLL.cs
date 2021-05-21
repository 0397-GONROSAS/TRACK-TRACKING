using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockTracking.DAL;
using StockTracking.DAL.DAO;
using StockTracking.DAL.DTO;

namespace StockTracking.BLL
{
    class EmployeeBLL : IBLL<EmployeeDetailDTO, EmployeeDTO>
    {
        EmployeeDAO employeedao = new EmployeeDAO();
        public bool Delete(EmployeeDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool GetBack(EmployeeDetailDTO entity)
        {
            throw new NotImplementedException();
        }

        public bool Insert(EmployeeDetailDTO entity)
        {
            EMPLOYEE employee = new EMPLOYEE();
            employee.Name = entity.Name;
            employee.UserNumber = entity.UserNumer;
            employee.Password = entity.Password;
            employee.Permission = entity.Permission;
            employee.Surname = entity.Surname;
            return employeedao.Insert(employee);
        }

        public EmployeeDTO Select()
        {
            EmployeeDTO dto = new EmployeeDTO();
            dto.Employees = employeedao.Select();
            dto.permission = employeedao.db.PERMISSIONS.ToList();
            return dto;
        }

        internal List<EmployeeDetailDTO> Get(int v, string text)
        {
            return employeedao.Get(v, text);
        }

        public bool Update(EmployeeDetailDTO entity)
        {
            EMPLOYEE emp = new EMPLOYEE();
            emp.UserNumber = entity.UserNumer;
            emp.Name = entity.Name;
            emp.Surname = entity.Surname;
            emp.Password = entity.Password;
            emp.Permission = entity.Permission;
            return employeedao.Update(emp);
        }
    }
}
