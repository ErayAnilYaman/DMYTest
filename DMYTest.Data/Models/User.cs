
namespace DMYTest.Data.Models
{
    #region Usings

    using System.Collections.Generic;
    using System.Diagnostics;

    #endregion

    public class User
    {
        private ICollection<UserOperationClaim> _userOperationClaim;
        private ICollection<Supplier> _suppliers;
        private ICollection<Employee> _employees;
        public User()
        {
            _userOperationClaim = new HashSet<UserOperationClaim>();
            _suppliers = new HashSet<Supplier>();
            _employees = new HashSet<Employee>();
        }
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool Status { get; set; }

        public virtual ICollection<UserOperationClaim> UserOperationClaim 
        { 
            get 
            { 
                return _userOperationClaim; 
            } 
            set 
            {
                this._userOperationClaim = value;
            }
        }
        public virtual ICollection<Supplier> Suppliers 
        {
            get 
            { 
                return this._suppliers; 
            } 
            set
            { 
                this._suppliers = value;
            }
        }
        public virtual ICollection<Employee> Employees 
        {
            get 
            {
                return this._employees;
            }
            set 
            {
                this._employees = value;
            }
        }
    }
}