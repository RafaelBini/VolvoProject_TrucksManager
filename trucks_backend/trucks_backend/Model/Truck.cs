namespace trucks_backend.Model
{
    public class Truck
    {
        public Truck()
        {
            ModelName = "FM 132";
            ModelYear = DateTime.Today.Year;
            ManufacturingYear = DateTime.Today.Year;
        }

        public int Id { get; set; }
        public string? ModelName { get; set; }
        public int ModelYear { get; set; }
        public int ManufacturingYear { get; set; }        
    }
}
