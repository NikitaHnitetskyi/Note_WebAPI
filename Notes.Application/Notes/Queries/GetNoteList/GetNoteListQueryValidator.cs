using FluentValidation;
using Notes.Application.Notes.Commands.DeleteCommand;
using Notes.Application.Notes.Queries.GetNoteDetails;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class GetNoteDetailsQueryValidator : AbstractValidator<GetNoteDetailsQuery>
    {
        public GetNoteDetailsQueryValidator()
        {
            RuleFor(x => x.UserId).NotEqual(Guid.Empty);
            RuleFor(note => note.UserId).NotEqual(Guid.Empty);
        }
    }
}
