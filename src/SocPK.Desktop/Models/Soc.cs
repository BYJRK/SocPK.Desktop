namespace SocPK.Desktop.Models
{
    public sealed class Soc
    {
        public Soc(string brand, string model, double score)
        {
            Brand = brand;
            Model = model;
            Score = score;
        }

        public Soc()
        {

        }

        public string Brand { get; set; }
        public string Model { get; set; }
        public double Score { get; set; }

        public override string ToString()
        {
            return $"{Brand} {Model} {Score}";
        }
    }
}
