namespace CarParts.Models.ViewModels
{
    public class CarPartListViewModel
    {

        public IEnumerable<CarPart> CarParts { get; set; }

        public PagingInfoViewModel PagingInfo { get; set; }
    }
}
