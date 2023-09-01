
namespace DMYTest.Data.Models
{
    public class UserOperationClaim
    {
        public int UserOperationClaimID { get; set; }
        public int UserID { get; set; }
        public int OperationClaimID { get; set; }
        public virtual OperationClaim OperationClaim { get; set; }
        public virtual User User { get; set; }
    }
}