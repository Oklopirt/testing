using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.Courses.Testing.Implementations;
using NUnit.Framework;

namespace Kontur.Courses.Testing
{
	public class WordsStatistics_Tests
	{
		public Func<IWordsStatistics> createStat = () => new WordsStatistics_CorrectImplementation(); // меняется на разные реализации при запуске exe
		public IWordsStatistics stat;

		[SetUp]
		public void SetUp()
		{
			stat = createStat();
		}

		[Test]
		public void no_stats_if_no_words()
		{
			CollectionAssert.IsEmpty(stat.GetStatistics());
		}

		[Test]
		public void same_word_twice()
		{
			stat.AddWord("xxx");
			stat.AddWord("xxx");
			CollectionAssert.AreEqual(new[] { Tuple.Create(2, "xxx") }, stat.GetStatistics());
		}

		[Test]
		public void single_word()
		{
			stat.AddWord("hello");
			CollectionAssert.AreEqual(new[] { Tuple.Create(1, "hello") }, stat.GetStatistics());
		}

		[Test]
		public void two_same_words_one_other()
		{
			stat.AddWord("hello");
			stat.AddWord("world");
			stat.AddWord("world");
			CollectionAssert.AreEqual(new[] { Tuple.Create(2, "world"), Tuple.Create(1, "hello") }, stat.GetStatistics());
		}

        [Test]
        public void by_descending () {
            stat.AddWord("o");
            stat.AddWord("zrrR");
            stat.AddWord("Yy");
            stat.AddWord("tttt");
            stat.AddWord("ttTt");
            stat.AddWord("yy");
            stat.AddWord("Tttt");
            stat.AddWord("zrrr");
            stat.AddWord("zRrr");
            CollectionAssert.AreEqual(new[] {Tuple.Create(3, "tttt"), Tuple.Create(3, "zrrr"), Tuple.Create(2, "yy"), Tuple.Create(1, "o")}, stat.GetStatistics());
        }

	    [Test]
	    public void empty_and_null_string()
	    {
            stat.AddWord("");
            stat.AddWord(null);
            CollectionAssert.AreEqual(new Tuple<int, string>[] {}, stat.GetStatistics());
        }

        [Test]
        public void static_dictionary() {
            stat.AddWord("hello");
            stat.AddWord("world");
            stat.AddWord("world");
            var stat2 = createStat();
            stat.AddWord("world");
            stat2.AddWord("world");
            CollectionAssert.AreEqual(new[] { Tuple.Create(1, "world") }, stat2.GetStatistics());
        }


        [Test]
        public void two_same_words_in_different_registers()
        {
            stat.AddWord("HelLo");
            stat.AddWord("HellO");
            stat.AddWord("heLLo");
            CollectionAssert.AreEqual(new[] { Tuple.Create(3, "hello") }, stat.GetStatistics());
        }

        //[Test]
        //public void words_more_than_ten_symbol()
        //{
        //    stat.AddWord("abacabadaba");
        //    stat.AddWord("abAcabadabA");
        //    stat.AddWord("abacAbaDaba");
        //    CollectionAssert.AreEqual(new[] { Tuple.Create(3, "abacabadab") }, stat.GetStatistics());
        //}

        [Test]
        public void words_more_than_ten_symbol_lex_oreder()
        {
            stat.AddWord("abacabadaba");
            stat.AddWord("abAcabadabA");
            stat.AddWord("abacAbaDaba");
            stat.AddWord("**--");
            CollectionAssert.AreEqual(new[] { Tuple.Create(3, "abacabadab"), Tuple.Create(1, "**--") }, stat.GetStatistics());
        }

        [Test]
        public void words_more_than_ten_symbol_lex_oreder2()
        {
            stat.AddWord("abacabadaba");
            stat.AddWord("abAcabadabA");
            stat.AddWord("abacAbaDaba");
            stat.AddWord("**--");
            stat.AddWord("**--");
            stat.AddWord("**--");
            CollectionAssert.AreEqual(new[] { Tuple.Create(3, "**--"), Tuple.Create(3, "abacabadab") }, stat.GetStatistics());
        }

        [Test]
        public void check_by_descending()
        {
            stat.AddWord("bb");
            stat.AddWord("bb");
            stat.AddWord("bb");
            stat.AddWord("aa");
            stat.AddWord("aa");
            stat.AddWord("aa");
            stat.AddWord("aa");
            CollectionAssert.AreEqual(new[] { Tuple.Create(4, "aa") , Tuple.Create(3, "bb")}, stat.GetStatistics());
        }

        [Test]
        public void check_by_descending_with_register()
        {
            stat.AddWord("Bb");
            stat.AddWord("bB");
            stat.AddWord("bb");
            stat.AddWord("aA");
            stat.AddWord("Aa");
            stat.AddWord("aa");
            stat.AddWord("aa");
            CollectionAssert.AreEqual(new[] { Tuple.Create(4, "aa"), Tuple.Create(3, "bb") }, stat.GetStatistics());
        }

        [Test]
        public void check_same_frequency2()
        {
            stat.AddWord("bb");
            stat.AddWord("bb");
            stat.AddWord("bb");
            stat.AddWord("bb");
            stat.AddWord("aa");
            stat.AddWord("aa");
            stat.AddWord("aa");
            stat.AddWord("aa");
            CollectionAssert.AreEqual(new[] { Tuple.Create(4, "aa"), Tuple.Create(4, "bb") }, stat.GetStatistics());
        }

        [Test]
        public void check_same_frequency_with_register()
        {
            stat.AddWord("Bbb");
            stat.AddWord("bBb");
            stat.AddWord("Bbb");
            stat.AddWord("bbB");
            stat.AddWord("aaa");
            stat.AddWord("aaA");
            stat.AddWord("aaa");
            stat.AddWord("AAa");
            CollectionAssert.AreEqual(new[] { Tuple.Create(4, "aaa"), Tuple.Create(4, "bbb") }, stat.GetStatistics());
        }

        [Test]
        public void empty_words()
        {
            stat.AddWord("");
            stat.AddWord("");
            stat.AddWord("");

            CollectionAssert.AreEqual(new Tuple<int, string>[]{}, stat.GetStatistics());
        }

        [Test]
        public void nulls_words()
        {
            stat.AddWord(null);
            stat.AddWord(null);
            stat.AddWord(null);

            CollectionAssert.AreEqual(new Tuple<int, string>[] { }, stat.GetStatistics());
        }

        [Timeout(2000)]
        [Test]
        public void hundred_thouthand_words_will_be_added()
        {
            var mystats = new Dictionary<string, int>();
            var rnd = new Random();
            for (int i = 0; i < 13000; i++)
            {
                var word = "";
                var len = rnd.Next(1, 20);
                for (int j = 0; j < len; j++)
                {
                    word += (char) rnd.Next(26);
                }
                stat.AddWord(word);
                if (string.IsNullOrEmpty(word)) return;
                if (word.Length > 10) word = word.Substring(0, 10);
                int count;
                mystats[word.ToLower()] = mystats.TryGetValue(word.ToLower(), out count) ? count + 1 : 1;
            }

            var answer = mystats.OrderByDescending(kv => kv.Value).ThenBy(kv => kv.Key).Select(kv => Tuple.Create(kv.Value, kv.Key));

            CollectionAssert.AreEqual(answer, stat.GetStatistics());
        }

        //[Timeout(5000)]
        //[Test]
        //public void million_words_will_be_added()
        //{
        //    var mystats = new Dictionary<string, int>();
        //    var rnd = new Random();
        //    for (int i = 0; i < 100000; i++)
        //    {
        //        var word = "";
        //        var len = rnd.Next(1, 20);
        //        for (int j = 0; j < len; j++)
        //        {
        //            word += (char)rnd.Next(3);
        //        }
        //        stat.AddWord(word);
        //        if (string.IsNullOrEmpty(word)) return;
        //        if (word.Length > 10) word = word.Substring(0, 10);
        //        int count;
        //        mystats[word.ToLower()] = mystats.TryGetValue(word.ToLower(), out count) ? count + 1 : 1;
        //    }

        //    var answer = mystats.OrderByDescending(kv => kv.Value).ThenBy(kv => kv.Key).Select(kv => Tuple.Create(kv.Value, kv.Key));

        //    CollectionAssert.AreEqual(answer, stat.GetStatistics());
        //}
	}
}