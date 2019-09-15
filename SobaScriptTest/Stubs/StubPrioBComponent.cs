using net.r_eg.SobaScript.Components;

namespace SobaScriptTest.Stubs
{
    internal sealed class StubPrioBComponent: ComponentAbstract
    {
        private TPrioAB prio;
        private bool _beforeDeepening;

        public override string Activator => "B ";

        public override bool BeforeDeepening => _beforeDeepening;

        public override string Eval(string data) => $"B{++prio.num}B>{data}<B";

        public StubPrioBComponent(TPrioAB data, bool beforeDeepening)
        {
            prio = data;
            _beforeDeepening = beforeDeepening;
        }
    }
}