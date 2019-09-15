using net.r_eg.SobaScript;
using net.r_eg.SobaScript.Exceptions;
using net.r_eg.Varhead;
using SobaScriptTest.Stubs;
using Xunit;

namespace SobaScriptTest
{
    public class SobaTest
    {
        private static IUVars uvars = new UVars();

        [Fact]
        public void ActivatorTest1()
        {
            var target = SobaAcs.MakeNewCoreOnly();

            Assert.Equal
            (
                $"#[{nameof(StubBoxComponent)} ...]", 
                target.Eval($"##[{nameof(StubBoxComponent)} ...]")
            );

            Assert.Equal
            (
                $"[{nameof(StubBoxComponent)} ...]",
                target.Eval($"#[{nameof(StubBoxComponent)} ...]")
            );
        }

        [Fact]
        public void ActivatorTest2()
        {
            Assert.Throws<MismatchException>(() =>
            {
                var target = SobaAcs.MakeNewCoreOnly();
                target.Eval("#[NotRealComponent prop.Test]");
            });
        }

        [Fact]
        public void ActivatorTest3()
        {
            var target = SobaAcs.MakeNewCoreOnly();

            Assert.Equal
            (
                $"[{nameof(StubBoxComponent)} ...]",
                target.Eval($"[{nameof(StubBoxComponent)} ...]")
            );

            Assert.Equal
            (
                $"{nameof(StubBoxComponent)} ...",
                target.Eval($"{nameof(StubBoxComponent)} ...")
            );

            Assert.Equal(" test ", target.Eval(" test "));
            Assert.Equal(" ", target.Eval(" "));
            Assert.Equal(string.Empty, target.Eval(string.Empty));
            Assert.Equal(" \"test\" ", target.Eval(" \"test\" "));
        }

        [Fact]
        public void ContainerTest1()
        {
            var target = SobaAcs.MakeNewCoreOnly();

            Assert.Equal("ne]", target.Eval("#[var name = value\nli]ne]"));
            Assert.Equal(string.Empty, target.Eval("#[var name = <#data>value\nli]ne</#data>]"));
            Assert.Equal(string.Empty, target.Eval("#[var name = left [box1] right]"));
            
        }

        [Fact]
        public void ContainerTest2()
        {
            var target = SobaAcs.MakeNewCoreOnly();

            Assert.Equal("#[var name = left [box1 right]", target.Eval("#[var name = left [box1 right]"));
            Assert.Equal(string.Empty, target.Eval("#[var name = \"left [box1 right\"]"));
        }

        [Fact]
        public void ContainerTest3()
        {
            var target = SobaAcs.MakeNewCoreOnly();

            Assert.Equal("test - cc", target.Eval("#[var sres = <#data>Data1</#data>]test - cc#[var sres2 = <#data>Data2</#data>]"));
            Assert.Equal("test - cc", target.Eval("#[var sres = <#data>Data1\n\nEnd</#data>]test - cc#[var sres2 = <#data>Data2\n\nEnd</#data>]"));
        }

        [Fact]
        public void PriorityTest1()
        {
            var soba = new Soba();

            soba.Register(new StubUserVariableComponent(soba));
            soba.Register(new StubBoxComponent(soba));

            Assert.Equal
            (
                string.Empty,
                soba.Eval($"#[var name = #[{nameof(StubBoxComponent)} ...] ]")
            );

            Assert.Equal
            (
                $"[{nameof(StubBoxComponent)} ......]",
                soba.Eval($"#[{nameof(StubBoxComponent)} ...#[var name = 123]...]")
            );
        }

        [Fact]
        public void PriorityTest2()
        {
            var soba = new Soba();
            var prio = new TPrioAB();

            soba.Register(new StubPrioAComponent(prio, false));
            soba.Register(new StubPrioBComponent(prio, false));

            Assert.Equal
            (
                "A2A>[A ... = B1B>[B ...]<B ]<A",
                soba.Eval("#[A ... = #[B ...] ]")
            );
        }

        [Fact]
        public void PriorityTest3()
        {
            var soba = new Soba();
            var prio = new TPrioAB();

            soba.Register(new StubPrioBComponent(prio, false));
            soba.Register(new StubPrioAComponent(prio, false));

            Assert.Equal
            (
                "A2A>[A ... = B1B>[B ...]<B ]<A",
                soba.Eval("#[A ... = #[B ...] ]")
            );
        }

        [Fact]
        public void PriorityTest4()
        {
            var soba = new Soba();
            var prio = new TPrioAB();

            soba.Register(new StubPrioAComponent(prio, true));
            soba.Register(new StubPrioBComponent(prio, false));

            Assert.Equal
            (
                "A1A>[A ... = #[B ...] ]<A",
                soba.Eval("#[A ... = #[B ...] ]")
            );
        }

        [Fact]
        public void PriorityTest5()
        {
            var soba = new Soba();
            var prio = new TPrioAB();

            soba.Register(new StubPrioBComponent(prio, false));
            soba.Register(new StubPrioAComponent(prio, true));

            Assert.Equal
            (
                "A1A>[A ... = #[B ...] ]<A",
                soba.Eval("#[A ... = #[B ...] ]")
            );
        }

        [Fact]
        public void PriorityTest6()
        {
            var soba = new Soba();
            var prio = new TPrioAB();

            soba.Register(new StubPrioAComponent(prio, false));
            soba.Register(new StubPrioBComponent(prio, true));

            Assert.Equal
            (
                "A2A>[A ... = B1B>[B ...]<B ]<A",
                soba.Eval("#[A ... = #[B ...] ]")
            );
        }

        [Fact]
        public void PriorityTest8()
        {
            var soba = new Soba();
            var prio = new TPrioAB();

            soba.Register(new StubPrioBComponent(prio, true));
            soba.Register(new StubPrioAComponent(prio, false));

            Assert.Equal
            (
                "A2A>[A ... = B1B>[B ...]<B ]<A",
                soba.Eval("#[A ... = #[B ...] ]")
            );
        }

        [Fact]
        public void PriorityTest9()
        {
            var soba = new Soba();
            var prio = new TPrioAB();

            soba.Register(new StubPrioAComponent(prio, true));
            soba.Register(new StubPrioBComponent(prio, true));

            Assert.Equal
            (
                "A1A>[A ... = #[B ...] ]<A",
                soba.Eval("#[A ... = #[B ...] ]")
            );
        }

        [Fact]
        public void PriorityTest10()
        {
            var soba = new Soba();
            var prio = new TPrioAB();

            soba.Register(new StubPrioBComponent(prio, true));
            soba.Register(new StubPrioAComponent(prio, true));

            Assert.Equal
            (
                "A1A>[A ... = #[B ...] ]<A",
                soba.Eval("#[A ... = #[B ...] ]")
            );
        }

        [Fact]
        public void PostProcessingTest1()
        {
            var soba = new Soba();
            soba.Register(new StubPrioZComponent());

            Assert.Equal
            (
                Value.TRUE,
                soba.Eval($"#[{nameof(StubPrioZComponent)} ... ]", true)
            );

            Assert.Equal
            (
                Value.FALSE,
                soba.Eval($"#[{nameof(StubPrioZComponent)} ... ]", false)
            );
        }
    }
}
