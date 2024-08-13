using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MainProject.Datastore;
using MainProject.Datastore.DataStoreInterfaces;
using MainProject.Model;
using MainProject.ViewModel;
using MainProject.Services;
using MainProject.Services.Interfaces;
using Moq;

namespace MainProjectTest.Services
{
	public class AuthorServiceTest
	{
		private readonly Mock<IAuthorRepository> _authorRepository;
		private readonly Mock<AuthorService> _authorViewModelBuilder;
		private readonly List<AuthorModel> _authorModelList;

		public AuthorServiceTest()
		{
			_authorRepository = new Mock<IAuthorRepository>();
			_authorViewModelBuilder = new Mock<AuthorService>(_authorRepository.Object);

			_authorModelList = new List<AuthorModel> {
				new AuthorModel
				{
					author_id = 1,
					first_name = "mark",
					middle_name = "weber",
					last_name = "stuart"
				},
				new AuthorModel
				{
					author_id = 2,
					first_name = "william",
					middle_name = "patric",
					last_name = "Michael"
				},
				new AuthorModel
				{
					author_id = 3,
					first_name = "Samantha",
					middle_name = "",
					last_name = "donavon"
				}
			};

			_authorRepository.Setup(s => s.getAuthorById(1)).Returns(_authorModelList[0]);
			_authorRepository.Setup(s => s.getAuthorById(2)).Returns(_authorModelList[1]);
			_authorRepository.Setup(s => s.getAuthorById(3)).Returns(_authorModelList[2]);

			_authorRepository.Setup(s => s.getAuthorByBook(1)).Returns(_authorModelList);
		}

		[Theory]
		[InlineData(1, "mark weber stuart")]
		[InlineData(2, "william patric Michael")]
		public void MAKE_SURE_NAME_IS_FULL_WITH_MIDDLE_NAME(int id, string expected)
		{
			AuthorModel authorModel = _authorViewModelBuilder.Object.createAuthorModel(id);
			Assert.Equal(authorModel.full_name, expected);
		}

		[Fact]
		public void MAKE_SURE_NAME_IS_FULL_WITHOUT_MIDDLE_NAME()
		{
			AuthorModel authorModel = _authorViewModelBuilder.Object.createAuthorModel(3);
			Assert.Equal("Samantha donavon", authorModel.full_name);
		}

		[Fact]
		public void MAKE_SURE_NAMES_ARE_FULL_AFTER_COMBINING()
		{
			IEnumerable<AuthorModel> authorModels = _authorViewModelBuilder.Object.createAuthorModelBatch(1);
			Assert.Equal("mark", authorModels.ToList().ElementAt(0).first_name);
			Assert.Equal("mark weber stuart", authorModels.ToList().ElementAt(0).full_name);
			Assert.Equal("william", authorModels.ToList().ElementAt(1).first_name);
			Assert.Equal("william patric Michael", authorModels.ToList().ElementAt(1).full_name);
			Assert.Equal("Samantha", authorModels.ToList().ElementAt(2).first_name);
			Assert.Equal("Samantha donavon", authorModels.ToList().ElementAt(2).full_name);
		}
	}
}
