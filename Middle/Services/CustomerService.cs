using Dapper;
using Middle.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middle.Services
{
   public class CustomerService
    {
        private string connectionString;
        public CustomerService()
        {
            connectionString = ConfigurationManager.ConnectionStrings["Datapro"].ConnectionString;
        }
        public IEnumerable<Customer> GetAll()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.Query<Customer>("select * from Customer");
            }
        }
        public IEnumerable<Customer> GetByIdRange(string startId, string endId)
        {
            string sql = "select * from Customer" + GetIdCondition(startId, endId);
            var parameter = new
            {
                startId = new DbString { IsAnsi = true, IsFixedLength = false, Length = 50, Value = startId },
                endId = new DbString { IsAnsi = true, IsFixedLength = false, Length = 50, Value = endId },
                endIdLike = new DbString { IsAnsi = true, IsFixedLength = false, Length = 50, Value = endId + "%" }
            };
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.Query<Customer>(sql, parameter);
            }
        }
        public Customer GetById(string id) {
            string sql = "select * from Customer where Id=@id";
            var parameter = new
            {
                id = new DbString { IsAnsi = true, IsFixedLength = false, Length = 50, Value = id }
            };
            using (var conn = new SqlConnection(connectionString))
            {
                return conn.QuerySingleOrDefault<Customer>(sql, parameter);
            }
        }
        public ServiceResult Add(string id, string name)
        {
            Customer customer = GetById(id);
            if (customer != null)
                return new ServiceResult { IsSuccess = false, Message = $"客戶編號[{id}]已存在，新增失敗。" };
            string sql = "INSERT INTO Customer (Id, Name) VALUES (@id, @name)";
            var parameter = new
            {
                id = new DbString { IsAnsi = true, IsFixedLength = false, Length = 50, Value = id },
                name = new DbString { IsAnsi = true, IsFixedLength = false, Length = 500, Value = name }
            };
            try
            {
                using (var conn = new SqlConnection(connectionString))
                {
                    conn.Execute(sql, parameter);
                }
                return new ServiceResult { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new ServiceResult { IsSuccess = false, Message = $"新增失敗:{ex.Message}" };
            }
       
        }
        string GetIdCondition(string startId, string endId)
        {
            bool hasStartValue = !string.IsNullOrEmpty(startId);
            bool hasEndValue = !string.IsNullOrEmpty(endId);
            string result = string.Empty;
            if (!hasStartValue && !hasEndValue)
                return result;
            result = " WHERE ";
            if (hasStartValue && hasEndValue)
                result += "Id >= @startId AND Id <= @endId";
            else if (hasStartValue)
                result += "Id >= @startId";
            else
                result += "Id <= @endId";
            if (hasEndValue)
                result += " OR Id LIKE @endIdLike";
            return result;
        }
    }
}

