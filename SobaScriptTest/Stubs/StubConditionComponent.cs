using net.r_eg.SobaScript;
using net.r_eg.SobaScript.Components;

namespace SobaScriptTest.Stubs
{
    internal sealed class StubConditionComponent: ComponentAbstract
    {
        public override string Activator => nameof(StubConditionComponent);

        public override string Eval(string data) => data;

        public StubConditionComponent()
        {

        }

        public StubConditionComponent(ISobaScript soba)
        {

        }
    }
}