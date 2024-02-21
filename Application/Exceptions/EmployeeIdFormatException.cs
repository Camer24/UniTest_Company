using FluentValidation.Results;
using System.Runtime.ConstrainedExecution;

namespace Application.Exceptions
{
    public class EmployeeIdFormatException : Exception
    {
        public static string DESCRIPTION = "Formato del id del empleado invalido.";
        public EmployeeIdFormatException() : base(DESCRIPTION) { }
    }
}
