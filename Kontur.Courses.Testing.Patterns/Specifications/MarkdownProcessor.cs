using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Kontur.Courses.Testing.Patterns.Specifications
{
	public class MarkdownProcessor
	{
		public string Render(string input)
		{
            var emReplacer = new Regex(@"([^\w\\]|^)_(.*?[^\\])_(\W|$)");
            var strongReplacer = new Regex(@"([^\w\\]|^)__(.*?[^\\])__(\W|$)");
            input = strongReplacer.Replace(input,
                    match => match.Groups[1].Value +
                            "<strong>" + match.Groups[2].Value + "</strong>" +
                            match.Groups[3].Value);
            input = emReplacer.Replace(input,
                    match => match.Groups[1].Value +
                            "<em>" + match.Groups[2].Value + "</em>" +
                            match.Groups[3].Value);
            input = input.Replace(@"\_", "_");
            return input;
		}
	}

	[TestFixture]
	public class MarkdownProcessor_should
	{
		private readonly MarkdownProcessor md = new MarkdownProcessor();

	    [Test]
	    public void not_change_text()
	    {
	        var input = "klasjd;lkfja asdfas";
            Assert.AreEqual("klasjd;lkfja asdfas", md.Render(input));
	    }

	    [Test]
	    public void insert_em()
	    {
	        var input = "qwerqwer _kljfkj_ sdfasdf";

            Assert.AreEqual(@"qwerqwer <em>kljfkj</em> sdfasdf", md.Render(input));
	    }

        [Test]
        public void insert_strong () {
            var input = "qwerqwer __kljfkj__ sdfasdf";

            Assert.AreEqual(@"qwerqwer <strong>kljfkj</strong> sdfasdf", md.Render(input));
        }

	    [TestCase("a_a_a", Result = "a_a_a", TestName = "emTag")]
        [TestCase("a__a__a", Result = "a__a__a", TestName = "strongTag")]
        [TestCase(@"a \_a\_ \_a", Result = @"a _a_ _a", TestName = "screeningForEm")]
        [TestCase(@"a \__a\__ \__a", Result = @"a __a__ __a", TestName = "screeningForStrong")]
        [TestCase(@"a \__a\__ \__a", Result = @"a __a__ __a", TestName = "screeningForStrong")]
        [TestCase(@"1_2_3", Result = @"1_2_3", TestName = "emTagBetweenDigits")]
        [TestCase(@"1 \_2\_ 3", Result = @"1 _2_ 3", TestName = "emTagBetweenDigits")]
        public string not_change_with (string input)
	    {
            
	        return md.Render(input);
	    }

	    [Test]
	    public void change_pair_brackets()
	    {
	        var input = "sad _sad__ sadaads";
	        var input2 = "asds _asdsd sdad __sdsdfsd_ sdsdfs__";
            Assert.AreEqual("sad <em>sad_</em> sadaads", md.Render(input2));
	    }
	}
}
