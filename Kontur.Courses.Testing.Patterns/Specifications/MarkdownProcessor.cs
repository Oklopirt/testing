using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Kontur.Courses.Testing.Patterns.Specifications
{
	public class MarkdownProcessor
	{
		public string Render(string input)
		{
		    //var emReplacer = new Regex("@(\W)_(.*?)_(\W)");
		}
	}

	[TestFixture]
	public class MarkdownProcessor_should
	{
		private readonly MarkdownProcessor md = new MarkdownProcessor();

	    [Test]
	    void em()
	    {

	    }
	}
}
