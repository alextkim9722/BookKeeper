﻿using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Services
{
	public class GenreTheoryDataGenerator : TheoryData<Genre>
	{
		public GenreTheoryDataGenerator()
		{
			for (int i = 0; i < 20; i++)
			{
				Genre genre = TestDatabaseGenerator.genreTable[i];
				genre.books = TestDatabaseGenerator.bookTable.Where(
						x => TestDatabaseGenerator.bookGenreTable
						.Where(y => y.genre_id == i + 1)
						.Select(y => y.book_id)
						.ToList()
						.Contains(x.book_id))
						.ToList();

				Add(genre);
			}
		}
	}
}
