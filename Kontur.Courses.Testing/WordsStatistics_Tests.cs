﻿using System;
using System.Collections.Generic;
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
        public void same_ditionary () {
            stat.AddWord("hello");
            stat.AddWord("world");
            stat.AddWord("world");
            stat = createStat();
            stat.AddWord("world");
            CollectionAssert.AreEqual(new[] { Tuple.Create(1, "world")}, stat.GetStatistics());
        }

	}
}