namespace EquipmentCheckout.Domain.Dtos
{
    public class HoldResult
    {
        public bool Success { get; private set; }
        public string? Error { get; private set; }

        public static HoldResult Ok() => new() { Success = true };
        public static HoldResult Fail(string error) => new() { Success = false, Error = error };
        
    }
}