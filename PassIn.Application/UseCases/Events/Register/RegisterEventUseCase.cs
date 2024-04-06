using PassIn.Communication.Requests;
<<<<<<< HEAD
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
=======
using PassIn.Exceptions;
>>>>>>> 796eb8c06301e4da3eaca30f25d423904e3c03b8

namespace PassIn.Application.UseCases.Events.Register
{
    public class RegisterEventUseCase
    {
<<<<<<< HEAD
        public ResponseRegisteredEventJson Execute(RequestEventJson request)
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

            return new ResponseRegisteredEventJson
            {
                Id = entity.Id
            };
=======
        public void Execute(RequestEventJson request)
        {
            Validate(request);
>>>>>>> 796eb8c06301e4da3eaca30f25d423904e3c03b8
        }

        private void Validate(RequestEventJson request)
        {
            if(request.MaximumAttendees <= 0)
            {
                throw new ErrorOnValidationException("O número de pessoas é inválido.");
            }

            if(string.IsNullOrWhiteSpace(request.Title))
            {
                throw new ErrorOnValidationException("O título é inválido.");
            }

            if(string.IsNullOrWhiteSpace(request.Details))
            {
                throw new ErrorOnValidationException("A descrição é inválida.");
            }


        }
    }
}
