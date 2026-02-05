using FluentValidation;
using CamelApp.API.Models;

namespace CamelApp.API.Validators;

public class CamelValidator : AbstractValidator<Camel>{
    public CamelValidator(){
        RuleFor(x => x.Name).NotEmpty().MinimumLength(2).MaximumLength(100);   
        RuleFor(x => x.HumpCount).Must(x => x == 1 || x == 2);  
        RuleFor(x => x.Color).MaximumLength(50);
    }
}