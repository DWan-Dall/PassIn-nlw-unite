using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;
using PassIn.Infrastructure.Entities;

namespace PassIn.Application.UseCases.CheckIns.DoCheckIn
{
    public class DoAttendeeCheckInUseCase
    {
        private readonly PassInDbContext _dbContext;
        public DoAttendeeCheckInUseCase()
        {
            _dbContext = new PassInDbContext();
        }

        public ResponseRegisteredJson Execute(Guid attendeeId)
        {
            Validate(attendeeId);

            var entity = new CheckIn
            {
                Attendee_Id = attendeeId,
                Created_at = DateTime.UtcNow,
            };

            _dbContext.CheckIns.Add(entity);
            _dbContext.SaveChanges();

            return new ResponseRegisteredJson
            {
                Id = entity.Id,
            };
        }

        private void Validate(Guid attendeeId)
        {
            var existAttendee = _dbContext.Attendees.Any(attendee => attendee.Id == attendeeId);
            if (existAttendee == false) 
            {
                throw new NotFoundException("O participante com esse ID não foi encontrado.");
            }

            var existCheckIn = _dbContext.CheckIns.Any(ch => ch.Attendee_Id == attendeeId);
            if (existCheckIn)
            {
                throw new ConflictException("Participante não pode fazer checkin mais de uma vez no mesmo evento.");
            }
        }
    }
}
