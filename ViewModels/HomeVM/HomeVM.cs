using DianaApp.ViewModels.ProductVM;
using DianaApp.ViewModels.SliderVM;

namespace DianaApp.ViewModels.HomeVM
{
    public class HomeVM
    {
        public IEnumerable<SliderListItemVM>  Sliders { get; set; }
        public IEnumerable<ProductListItemVM> Products { get; set; }
    }
}
