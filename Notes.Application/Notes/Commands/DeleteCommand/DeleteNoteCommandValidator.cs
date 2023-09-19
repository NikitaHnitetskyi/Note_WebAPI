

using FluentValidation;

namespace Notes.Application.Notes.Commands.DeleteCommand
{
    public class DeleteNoteCommandValidator: AbstractValidator<DeleteNoteCommand>
    {
        public DeleteNoteCommandValidator() 
        {
            RuleFor(deleteNoteCommmand => deleteNoteCommmand.Id).NotEqual(Guid.Empty);
            RuleFor(deleteNoteCommand => deleteNoteCommand.UserId).NotEqual(Guid.Empty);
        }

    }
}
