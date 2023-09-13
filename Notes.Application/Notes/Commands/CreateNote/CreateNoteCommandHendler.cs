using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Notes.Application.Interfaces;
using Notes.Domain;


namespace Notes.Application.Notes.Commands.CreateNote
{
    public class CreateNoteCommandHendler : IRequestHandler<CreateNodeCommands, Guid>
    {
        private readonly INotesDbContext _dbContext;

        public CreateNoteCommandHendler(INotesDbContext dbContext) => _dbContext = dbContext;

        public async Task<Guid> Handle(CreateNodeCommands request, CancellationToken cancellationToken)
        {
            var note = new Note
            {
                UserId = request.UserId,
                Title = request.Title,
                Details = request.Details,
                Id = Guid.NewGuid(),
                CreationDate = DateTime.Now,
                EditDate = null
            };

            await _dbContext.Notes.AddAsync(note, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return note.Id;
        }
    }
}
