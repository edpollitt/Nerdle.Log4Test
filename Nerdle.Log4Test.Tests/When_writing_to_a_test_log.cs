using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using log4net;
using log4net.Core;
using NUnit.Framework;

namespace Nerdle.Log4Test.Tests
{
    [TestFixture]
    class When_writing_to_a_test_log
    {
        ILog _log;

        [OneTimeSetUp]
        public void BeforeFirst()
        {
            Log4TestConfigurator.Configure();
            _log = LogManager.GetLogger(GetType());
        }

        [SetUp]
        public void BeforeEach()
        {
            //_log.Clear();
        }

        [Test]
        public void The_log_contents_can_be_retrieved()
        {
            _log.Info("foo");
            _log.WarnFormat("{0}", "bar");
            _log.Error("baz", new InvalidOperationException());

            _log.Contents().Should().HaveCount(3);
        }

        [Test]
        public void An_individual_log_can_be_cleared()
        {
            var fooLog = LogManager.GetLogger("foo");
            var barLog = LogManager.GetLogger("bar");

            fooLog.Info("foo");
            barLog.Info("bar");

            fooLog.Clear();

            fooLog.Contents().Should().BeEmpty();
            barLog.Contents().Should().HaveCount(1);
        }

    }
}
