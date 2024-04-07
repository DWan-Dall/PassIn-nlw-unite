using Microsoft.EntityFrameworkCore;
using PassIn.Communication.Responses;
using PassIn.Exceptions;
using PassIn.Infrastructure;

namespace PassIn.Application.UseCases.Attendees.GetAllByEnventId
{
    public class GetAllAttendeesByEventIdUseCase
    {
        private readonly PassInDbContext _dbContext;
        public GetAllAttendeesByEventIdUseCase()
        {
            _dbContext = new PassInDbContext();
        }
        public ResponseAllAttendeesJson Execute(Guid eventId)
        {
            //var attendees = _dbContext.Attendees.Where(attendee => attendee.Event_Id == eventId).ToList();
            //var attendees = _dbContext.Events.Find(eventId);
            var entity = _dbContext.Events.Include(ev => ev.Attendees).ThenInclude(attendee => attendee.CheckIn).FirstOrDefault(ev => ev.Id == eventId);
            if (entity is null)
                throw new NotFoundException("O evento informado não existe.");

            return new ResponseAllAttendeesJson
            {
                Attendees = entity.Attendees.Select(attendee => new ResponseAttendeeJson
                {
                    Id = attendee.Id,
                    Name = attendee.Name,
                    Email = attendee.Email,
                    CreatedAt = attendee.Created_At,
                    CheckedInAt = attendee.CheckIn?.Created_at
                }).ToList()
            };
        }
    }
}
