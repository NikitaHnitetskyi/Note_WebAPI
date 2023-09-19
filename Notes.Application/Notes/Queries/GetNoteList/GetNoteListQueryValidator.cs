using FluentValidation;
using Notes.Application.Notes.Commands.DeleteCommand;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class GetNoteListQueryValidator : AbstractValidator<GetNoteListQuery>
    {
        public GetNoteListQueryValidator()
        {
            RuleFor(x => x.UserId)
        }
    }
}
