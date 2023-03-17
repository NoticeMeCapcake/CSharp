using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace WebAppMVC.Models;

public class Contact
{
    [Display(Name = "Введите имя")]
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    [StringLength(50, ErrorMessage = "Длина имени не должна превышать 50 символов")]
    
    public string Name { get; set; }
    [Display(Name = "Введите фамилию")]
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    public string Surname { get; set; }
    [Display(Name = "Введите возраст")]
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    public int Age { get; set; }
    [Display(Name = "Имя")]
    [Required(ErrorMessage = "Поле не должно быть пустым")]
    public string Email { get; set; }
    [Display(Name = "Описание")]
    public string message { get; set; }
}