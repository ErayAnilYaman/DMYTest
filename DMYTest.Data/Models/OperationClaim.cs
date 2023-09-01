
namespace DMYTest.Data.Models
{
    #region Usings
    using System.Collections;
    using System.Collections.Generic;


    #endregion

    public class OperationClaim
    {
        private ICollection<UserOperationClaim> _userOperationClaim;

        public OperationClaim()
        {
            _userOperationClaim = new HashSet<UserOperationClaim>();
        }
        public int OperationClaimID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserOperationClaim> UserOperationClaims 
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

    }
}
