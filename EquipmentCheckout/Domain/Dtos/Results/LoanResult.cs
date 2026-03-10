namespace EquipmentCheckout.Domain.Dtos.Results
{
	public class LoanResult
	{
		public bool Success { get; set; }
		public string? Error { get; set; }
		public int? LoanId { get; set; }

		public static LoanResult Ok(int loanId) => new() { Success = true, LoanId = loanId };
		public static LoanResult Fail(string error) => new() { Success = false, Error = error };
	}
}
