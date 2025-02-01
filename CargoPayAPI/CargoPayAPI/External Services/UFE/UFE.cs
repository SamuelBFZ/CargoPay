namespace CargoPayAPI.External_Services.UFE
{
    public class UFE : IUFE
    {
        private decimal _currentFee;
        private readonly Random _random;

        public UFE()
        {
            _currentFee = 1.0m; // Base Fee
            _random = new Random();
        }

        public decimal GetCurrentFee()
        {
            decimal randomFactor = (decimal)_random.NextDouble() * 2m; //Random number between 0 and 2
            _currentFee *= randomFactor;
            return _currentFee;
        }
    }
}
