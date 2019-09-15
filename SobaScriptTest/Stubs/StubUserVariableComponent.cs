using net.r_eg.SobaScript;
using net.r_eg.SobaScript.Components;

namespace SobaScriptTest.Stubs
{
    internal sealed class StubUserVariableComponent: ComponentAbstract
    {
        public override string Activator => "var ";

        public override string Eval(string data) => string.Empty;

        public StubUserVariableComponent()
        {

        }

        public StubUserVariableComponent(ISobaScript soba)
        {

        }
    }
}