using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Events.Register
{
    public class RegisterEventUseCase
    {
        public ResponseRegisteredJson Execute(RequestEventJson request)
        {
            Validate(request);

            var dbContext = new PassInDbContext();

            var entity = new Infrastructure.Entities.Event
            {
                Title = request.Title,
                Details = request.Details,
                Maximum_Attendees = request.MaximumAttendees,
                Slug = request.Title.ToLower().Replace(" ", "-"),
            };

            dbContext.Events.Add(entity);
            dbContext.SaveChanges();

            return new ResponseRegisteredJson
            {
                Id = entity.Id
            };
        }

        private static void Validate(RequestEventJson request)
        {
            if (request.MaximumAttendees <= 0)
            {
                throw new ErrorOnValidationException("O número de pessoas é inválido.");
            }

            if (string.IsNullOrWhiteSpace(request.Title))
            {
                throw new ErrorOnValidationException("O título é inválido.");
            }

            if (string.IsNullOrWhiteSpace(request.Details))
            {
                throw new ErrorOnValidationException("A descrição é inválida.");
            }

        }
    }
}

