using System;
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

            _log.Info("bork");
            _log.WarnFormat("{0}", "dork");
            _log.ErrorFormat("mork");
            _log.Error("nork", new InvalidOperationException());
            _log.Error("zork");
            _log.Fatal("cork");
            _log.FatalFormat("pork");
        }

        [Test]
        public void Log_events_can_be_examined()
        {
            _log.Events().Should().HaveCount(7);
        }

        [Test]
        public void Log_events_can_be_filtered_at_a_specified_level()
        {
            _log.Events().At(Level.Info).Should().HaveCount(1);
            _log.Events().At(Level.Error).Should().HaveCount(3);
            _log.Events().At(Level.Emergency).Should().BeEmpty();
        }

        [Test]
        public void Log_events_can_be_filtered_above_a_specified_level()
        {
            _log.Events().Above(Level.Error).Should().OnlyContain(e => e.Level == Level.Fatal);
        }

        [Test]
        public void Log_events_can_be_filtered_at_or_above_a_specified_level()
        {
            _log.Events().AtOrAbove(Level.Error).Should()
                .OnlyContain(e => e.Level == Level.Error || e.Level == Level.Fatal);
        }

        [Test]
        public void Log_events_can_be_filtered_below_a_specified_level()
        {
            _log.Events().Below(Level.Warn).Should().OnlyContain(e => e.Level == Level.Info);
        }

        [Test]
        public void Log_events_can_be_filtered_at_or_below_a_specified_level()
        {
            _log.Events().AtOrBelow(Level.Warn).Should()
                .OnlyContain(e => e.Level == Level.Warn || e.Level == Level.Info);
        }

        [Test]
        public void An_individual_log_can_be_cleared()
        {
            var fooLog = LogManager.GetLogger("foo");
            var barLog = LogManager.GetLogger("bar");

            fooLog.Info("foo");
            barLog.Info("bar");

            fooLog.Clear();

            fooLog.Events().Should().BeEmpty();
            barLog.Events().Should().HaveCount(1);
        }
    }
}
