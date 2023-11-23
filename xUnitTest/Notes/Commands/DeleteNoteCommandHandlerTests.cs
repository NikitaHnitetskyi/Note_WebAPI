using Notes.Application.Common.Exceptions;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.DeleteCommand;
using xUnitTest.Common;
using Notes.Persistence;
using Xunit;
using System.Threading;

namespace xUnitTest.Notes.Commands
{
    public class DeleteNoteCommandHandlerTest : TestCommandBase
    {
        [Fact]
        public async Task DeleteNoteCommandHandler_Succes()
        {
            //Arrage
            var handler = new DeleteNoteCommandHadnler(Context);

            //Act
            await handler.Handle(new DeleteNoteCommand
            {
                Id = NotesContextFactory.NoteIdForDelete,
                UserId = NotesContextFactory.UserAId
            }, CancellationToken.None);

            //Assert
            Assert.Null(Context.Notes.SingleOrDefault(note =>
            note.Id == NotesContextFactory.NoteIdForDelete));
        }

        public async Task DeleteNoteCommandHandler_FailOnWrongId()
        {
            //Arrage
            var handler = new DeleteNoteCommandHadnler(Context);
            //Act
            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async ()
                => await handler.Handle(
                    new DeleteNoteCommand
                    {
                        Id = Guid.NewGuid(),
                        UserId = NotesContextFactory.UserAId
                    },
                    CancellationToken.None));
        }
        [Fact]
        public async Task DeleteNoteCommandHendler_FailOnWrongUserId()
        {
            //Arrange
            var deleteHandler = new DeleteNoteCommandHadnler(Context);
            var createHandler = new CreateNoteCommandHendler(Context);
            var noteId = await createHandler.Handle(
                new CreateNodeCommands
                {
                    Title = "NoteTitle",
                    UserId = NotesContextFactory.UserAId
                }, CancellationToken.None);

            //Assert
            await Assert.ThrowsAsync<NotFoundException>(async () =>
            await deleteHandler.Handle(
                new DeleteNoteCommand
                {
                    Id = noteId,
                    UserId = NotesContextFactory.UserBId
                }, CancellationToken.None));
        }
    }
}
