using System.Text.RegularExpressions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace TesteICI.Domain.Business.Validations;

public static class ExtensionsValidation
{
    public static IRuleBuilderOptions<T, string> NomeDePessoaValido<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder.Must(NomeDePessoaValido)
                          .WithMessage("O nome deve ser um nome de pessoa válido.");

        bool NomeDePessoaValido(string nome)
        {
            // Verifica se o nome contém apenas letras, espaços e hífens, e tem pelo menos duas palavras
            var regexParaNome = @"^[a-zA-ZÀ-ÿ\s'-]+$";
            var palavras = nome.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return Regex.IsMatch(nome, regexParaNome) && palavras.Length >= 2;
        }
    }

    public static IRuleBuilderOptions<T, string> PossuiSenhaDentroDoPadrao<T>(this IRuleBuilder<T, string> ruleBuilder, PasswordOptions options)
    {
        var validation = ruleBuilder;

        if (options.RequiredLength > 0)
        {
            validation = validation.MinimumLength(options.RequiredLength).WithMessage($"A senha deve ter pelo menos {options.RequiredLength} caracteres.");
        }

        if (options.RequireDigit)
        {
            validation = validation.Matches("[0-9]").WithMessage("A senha deve conter pelo menos um número.");
        }

        if (options.RequireLowercase)
        {
            validation = validation.Matches("[a-z]").WithMessage("A senha deve conter pelo menos uma letra minúscula.");
        }

        if (options.RequireUppercase)
        {
            validation = validation.Matches("[A-Z]").WithMessage("A senha deve conter pelo menos uma letra maiúscula.");
        }

        if (options.RequireNonAlphanumeric)
        {
            validation = validation.Matches("[^a-zA-Z0-9]").WithMessage("A senha deve conter pelo menos um caracter especial.");
        }

        return (IRuleBuilderOptions<T, string>)validation;
    }
}
