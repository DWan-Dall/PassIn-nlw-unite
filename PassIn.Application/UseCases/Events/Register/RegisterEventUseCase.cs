using PassIn.Communication.Requests;
using PassIn.Exceptions;

namespace PassIn.Application.UseCases.Events.Register
{
    public class RegisterEventUseCase
    {
        public void Execute(RequestEventJson request)
        {
            Validate(request);
        }

        private void Validate(RequestEventJson request)
        {
            if(request.MaximumAttendees <= 0)
            {
                throw new PassInException("O número de pessoas é inválido.");
            }

            if(string.IsNullOrWhiteSpace(request.Title))
            {
                throw new PassInException("O título é inválido.");
            }

            if(string.IsNullOrWhiteSpace(request.Details))
            {
                throw new PassInException("A descrição é inválida.");
            }


        }
    }
}
