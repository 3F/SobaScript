using net.r_eg.SobaScript;
using net.r_eg.SobaScript.Components;

namespace SobaScriptTest.Stubs
{
    internal sealed class StubBoxComponent: ComponentAbstract
    {
        public override string Activator => nameof(StubBoxComponent);

        public override string Eval(string data) => data;

        public StubBoxComponent()
        {

        }

        public StubBoxComponent(ISobaScript soba)
        {

        }
    }
}