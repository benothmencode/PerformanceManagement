using ProjectF.ModelsDTOS;

namespace ProjectF.ViewModels
{
    public class Vote
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public UserEntityDto user { get; set; }



    }
}
