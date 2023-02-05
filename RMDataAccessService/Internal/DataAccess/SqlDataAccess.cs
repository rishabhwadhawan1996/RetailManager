using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper;

namespace RMDataAccessService.Internal.DataAccess
{
    /// <summary>
    /// class used to communicate with the database
    /// Should not be used by external code directly
    /// </summary>
    internal class SqlDataAccess : IDisposable
    {
        private bool isClosed;

        /// <summary>
        /// Returns connection string
        /// </summary>
        /// <param name="name">name of the connection string</param>
        /// <returns>connection string corresponding to the name passed</returns>
        public string GetConnectionString(string name)
        {
            return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }

        /// <summary>
        /// Returns requested data from db
        /// Implemented using dapper
        /// </summary>
        /// <typeparam name="T">generic type T</typeparam>
        /// <typeparam name="U">generic type U</typeparam>
        /// <param name="storedProcedure">stored Procedure</param>
        /// <param name="parameteres">parameters</param>
        /// <param name="connectionStringName">connection string name</param>
        /// <returns>list of generic type T</returns>
        public List<T> LoadData<T, U>(string storedProcedure, U parameteres, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(storedProcedure, parameteres,
                    commandType: CommandType.StoredProcedure).ToList();
                return rows;
            }
        }

        /// <summary>
        /// Save data in the database
        /// Implemented using Dapper
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="storedProcedure">stored procedure</param>
        /// <param name="parameteres">parameters</param>
        /// <param name="connectionStringName">connection string name</param>
        public void SaveData<T>(string storedProcedure, T parameteres, string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(storedProcedure, parameteres,
                    commandType: CommandType.StoredProcedure);
            }
        }

        private IDbConnection connection;
        private IDbTransaction transaction;

        public void StartTransaction(string connectionStringName)
        {
            string connectionString = GetConnectionString(connectionStringName);
            connection = new SqlConnection(connectionString);
            connection.Open();
            transaction = connection.BeginTransaction();
            isClosed = false;
        }

        /// <summary>
        /// Save data in the database
        /// Implemented using Dapper
        /// </summary>
        /// <typeparam name="T">Generic type</typeparam>
        /// <param name="storedProcedure">stored procedure</param>
        /// <param name="parameteres">parameters</param>
        public void SaveDataUsingTransaction<T>(string storedProcedure, T parameteres)
        {
            connection.Execute(storedProcedure, parameteres,
                    commandType: CommandType.StoredProcedure, transaction: transaction);
        }

        /// <summary>
        /// Returns requested data from db
        /// Implemented using dapper
        /// </summary>
        /// <typeparam name="T">generic type T</typeparam>
        /// <typeparam name="U">generic type U</typeparam>
        /// <param name="storedProcedure">stored Procedure</param>
        /// <param name="parameteres">parameters</param>
        /// <param name="connectionStringName">connection string name</param>
        /// <returns>list of generic type T</returns>
        public List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameteres)
        {
            List<T> rows = connection.Query<T>(storedProcedure, parameteres,
                    commandType: CommandType.StoredProcedure, transaction: transaction).ToList();
            return rows;
        }

        public void CommitTransaction()
        {
            transaction?.Commit();
            connection?.Close();
            isClosed = true;
        }

        public void RollbackTransaction()
        {
            transaction?.Rollback();
            connection?.Close();
            isClosed = true;
        }

        public void Dispose()
        {
            if (!isClosed)
            {
                try
                {
                    CommitTransaction();
                }
                catch
                {

                }
            }
            transaction = null;
            connection = null;
        }
    }
}
