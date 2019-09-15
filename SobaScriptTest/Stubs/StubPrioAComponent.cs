using net.r_eg.SobaScript.Components;

namespace SobaScriptTest.Stubs
{
    internal sealed class StubPrioAComponent: ComponentAbstract
    {
        private TPrioAB prio;
        private bool _beforeDeepening;

        public override string Activator => "A ";

        public override bool BeforeDeepening => _beforeDeepening;        

        public override string Eval(string data) => $"A{++prio.num}A>{data}<A";

        public StubPrioAComponent(TPrioAB data, bool beforeDeepening)
        {
            prio = data;
            _beforeDeepening = beforeDeepening;
        }
    }
}