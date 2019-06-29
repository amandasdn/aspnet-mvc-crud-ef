using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRUD_EF.Models
{
    public class Usuario
    {
        [Key]
        [Display(Name = "Código")]
        public int ID { get; set; }

        [Required(ErrorMessage = "Digite o nome do usuário.", AllowEmptyStrings = false)]
        [Display(Name = "Nome do Usuário")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Digite o e-mail do usuário.")]
        [Display(Name = "E-mail")]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "Informe um e-mail válido...")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite a senha.", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 4)]
        public string Senha { get; set; }

        [Required]
        [Display(Name = "Data de Cadastro")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime DataCadastro { get; set; }

        [Required]
        [Display(Name = "Nível de Acesso")]
        public int NivelAcesso { get; set; }
    }
}