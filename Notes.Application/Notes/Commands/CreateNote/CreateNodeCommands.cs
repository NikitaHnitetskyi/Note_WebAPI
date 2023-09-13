using System;
using MediatR;

namespace Notes.Application.Notes.Commands.CreateNote
{
    public class CreateNodeCommands : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
    }
}
