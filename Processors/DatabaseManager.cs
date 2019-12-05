using System;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Login.Models;

namespace Login.Processors
{
    public class DatabaseManager
    {
        internal AppDb Db { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        internal DatabaseManager(AppDb db)
        {
            Db = db;
        }

        public async Task InsertAsync(User user)
        {
            try
            {
                var cmd = Db.Connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO `Users` (`Username`, `Password`, `Created`, `Email`, `Phone`) 
                    VALUES (@Username, @Password, @Created, @Email, @Phone);";
                BindParams(cmd, user);
                Db.Connection.Open();
                await cmd.ExecuteNonQueryAsync();
                //Id = (int)cmd.LastInsertedId;
            }
            catch (Exception ex)
            {

            }
            finally {
                if (Db.Connection.State.Equals(ConnectionState.Open))
                    Db.Connection.Close();
            }
        }

        //This should call a stored proc that checks if a given password matches the DB
        public async Task GetAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM `Users` WHERE `Id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        //Will use this to update email, phone, etc.
        public async Task UpdateAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            // cmd.CommandText = @"UPDATE `BlogPost` SET `Title` = @title, `Content` = @content WHERE `Id` = @id;";
            BindParams(cmd);
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        //Soft delete
        public async Task DeleteAsync()
        {
            using var cmd = Db.Connection.CreateCommand();
            //cmd.CommandText = @"DELETE FROM `BlogPost` WHERE `Id` = @id;";
            //Set IsActive to 0
            BindId(cmd);
            await cmd.ExecuteNonQueryAsync();
        }

        private void BindId(MySqlCommand cmd)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }

        private void BindParams(MySqlCommand cmd, User user = null)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Username",
                DbType = DbType.String,
                Value = user.UserName
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Password",
                DbType = DbType.String,
                Value = user.Password
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Created",
                DbType = DbType.DateTime,
                Value = DateTime.Now
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Email",
                DbType = DbType.String,
                Value = user.UserName //TODO change this if necessary 
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Phone",
                DbType = DbType.String,
                Value = user.Phone
            });
        }
    }

    public class AppDb
    {
        public MySqlConnection Connection { get; }

        public AppDb(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        // public void Dispose() => Connection.Dispose();
    }
}