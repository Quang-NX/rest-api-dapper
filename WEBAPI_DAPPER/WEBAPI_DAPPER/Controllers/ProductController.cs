using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WEBAPI_DAPPER.Model;

namespace WEBAPI_DAPPER.Controllers
{
    public class ProductController : BaseController
    {
        private readonly string _connectionString;
        public ProductController(ILogger<ProductController> logger, IConfiguration configuration) : base(logger)
        {
            _connectionString = configuration.GetConnectionString("DbConnectionString");
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                var result = await connection.QueryAsync<Product>("Get_Product_All", null, null, null, System.Data.CommandType.StoredProcedure);
                return result;
            }
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<Product> Get(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                var paramaters = new DynamicParameters();
                paramaters.Add("@id", id);
                var result = await connection.QueryAsync<Product>("Get_Product_By_Id", paramaters, null, null, System.Data.CommandType.StoredProcedure);
                return result.SingleOrDefault();
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<int> Post([FromBody] Product request)
        {
            int newId = 0;
            using (var connection = new SqlConnection(_connectionString))
            {
                if (connection.State == System.Data.ConnectionState.Closed)
                {
                    connection.Open();
                }
                var paramaters = new DynamicParameters();

                paramaters.Add("@sku", request.Sku);
                paramaters.Add("@price", request.Price);
                paramaters.Add("@discountPrice", request.DiscountPrice);
                paramaters.Add("@isActive", request.IsActive);
                paramaters.Add("@imageUrl", request.ImageUrl);
                paramaters.Add("@id", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);

                var result = await connection.ExecuteAsync("Create_Product", paramaters, null, null, System.Data.CommandType.StoredProcedure);

                newId = paramaters.Get<int>("@id");
            }
            return newId;
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
