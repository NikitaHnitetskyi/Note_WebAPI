using AutoMapper;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Persistence;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using xUnitTest.Common;

namespace xUnitTest.Notes.Queries
{
    public class GetNoteDetailsListQueryHandlerTest
    {
        [Collection("QueryCollection")]
        public class GetNoteDetailsQueryHandlerTests
        {
            private readonly NotesDbContext Context;
            private readonly IMapper Mapper;

            public GetNoteDetailsQueryHandlerTests(QueryTestFixture fixture)
            {
                Context = fixture.Context;
                Mapper = fixture.Mapper;
            }

            [Fact]
            public async Task GetNoteDetailsQueryHandler_Success()
            {
                //Arrange 
                var handler = new GetNoteDetailsQueryHandler(Context, Mapper);

                //Act
                var result = await handler.Handle(
                    new GetNoteDetailsQuery
                    {
                        UserId = NotesContextFactory.UserBId,
                        Id = Guid.Parse("")
                    },
                    CancellationToken.None);

                //Assert
                result.ShouldBeOfType<NoteDetailsVm>();
                result.Title.ShouldBe("Title2");
                result.CreationDate.ShouldBe(DateTime.Today);
            }
        }
    }
}
