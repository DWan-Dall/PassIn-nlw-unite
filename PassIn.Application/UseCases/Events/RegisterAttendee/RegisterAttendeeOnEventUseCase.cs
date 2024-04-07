using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;
using System.Net.Http.Headers;
using System.Net.Mail;

namespace PassIn.Application.UseCases.Events.RegisterAttendee
{
    public class RegisterAttendeeOnEventUseCase
    {
        private readonly PassInDbContext _dbContext;
        public RegisterAttendeeOnEventUseCase()
        {
            _dbContext = new PassInDbContext();
        }

        public ResponseRegisteredJson Execute(Guid eventId, RequestRegisterEventJson request)
        {
            Validate(eventId, request);

            var entity = new Infrastructure.Entities.Attendee
            {
                Email = request.Email,
                Name = request.Name,
                Event_Id = eventId,
                Created_At = DateTime.UtcNow,
            };

            _dbContext.Attendees.Add(entity);
            _dbContext.SaveChanges();

            return new ResponseRegisteredJson
            {
                Id = entity.Id,
            };
        }

        private void Validate(Guid eventId, RequestRegisterEventJson request)
        {
            var eventEntity = _dbContext.Events.Find(eventId);
            if (eventEntity is null)
                throw new NotFoundException("Evento não exixtente.");

            if (string.IsNullOrWhiteSpace(request.Name))
            {
                throw new ErrorOnValidationException("Nome inválido");
            }

            var emailIsValid = EmailIsValid(request.Email);
            if (emailIsValid == false)
            {
                throw new ErrorOnValidationException("E-mail inválido.");
            }

            var attendeeAlreadyRegistered = _dbContext
                .Attendees
                .Any(attendee => attendee.Email.Equals(request.Email) && attendee.Event_Id == eventId);

            if (attendeeAlreadyRegistered)
            {
                throw new ConflictException("Esse e-mail já está registrado nesse evento!");
            }

            var attendeesForEvent = _dbContext.Attendees.Count(attendee => attendee.Event_Id == eventId);
            if(attendeesForEvent == eventEntity.Maximum_Attendees)
            {
                throw new ErrorOnValidationException("Número máximo de participantes atingido!");
            }
        }

        private bool EmailIsValid(string  email)
        {
            try
            {
                new MailAddress(email);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
