using System.ComponentModel.DataAnnotations;
using Login.Processors;
 
namespace Login.Models
{
    public class User
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }        
 
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Phone Number")]
        public string Phone { get; set; }
 
        [Display(Name = "Remember on this computer")]
        public bool RememberMe { get; set; }
        
        /// <summary>
        /// Checks if user with given password exists in the database
        /// </summary>
        /// <param name="_username">User name</param>
        /// <param name="_password">User password</param>
        /// <returns>True if user exist and password is correct</returns>
        public bool IsValid(string _username, string _password, AppDb db)
        {
            var dbMgr = new DatabaseManager(db);
            return dbMgr.IsPasswordValid(_username, _password);
        }
    }
}