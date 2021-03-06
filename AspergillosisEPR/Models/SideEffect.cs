using AspergillosisEPR.Data;
using System.ComponentModel.DataAnnotations;
using System.Linq;
namespace AspergillosisEPR.Models
{
    public class SideEffect
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Side Effect Name is required")]
        public string Name { get; set; }
        public string KlassName => typeof(SideEffect).Name;
    }
}
