using net.r_eg.SobaScript;
using net.r_eg.SobaScript.Components;

namespace SobaScriptTest.Stubs
{
    internal sealed class StubPrioZComponent: ComponentAbstract
    {
        public override string Activator => nameof(StubPrioZComponent);

        public override string Eval(string data) => Value.From(PostProcessing);

        public StubPrioZComponent()
        {

        }
    }
}