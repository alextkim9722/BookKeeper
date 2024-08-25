using BackEnd.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndTest.Services.Comparator
{
	public static class ErrorComparator
	{
		public static void CompareErrors(string left, string right)
		{
			Assert.NotNull(right);
			Assert.Equal("[ERROR]: " + left + Environment.NewLine, right);
		}
	}
}
