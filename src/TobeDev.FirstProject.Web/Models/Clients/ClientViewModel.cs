using System.ComponentModel.DataAnnotations;

namespace TobeDev.FirstProject.Web.Models.Clients
{
    public class ClientViewModel
    {
        public int Id { get; set; }




        [Required]
        [MaxLength(20)]
        [Display(Name = "Nome")]
        public string FirstName { get; set; }



        [Required]
        [MaxLength(20)]
        [Display(Name = "Sobrenome")]
        public string LastName { get; set; }






        [Display(Name = "Data de Nascimento")] 
        public DateTime BirthDate { get; set; }
    }
}
