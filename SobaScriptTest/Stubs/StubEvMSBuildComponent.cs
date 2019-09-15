using net.r_eg.SobaScript;
using net.r_eg.SobaScript.Components;

namespace SobaScriptTest.Stubs
{
    internal sealed class StubEvMSBuildComponent: ComponentAbstract
    {
        public override string Activator => nameof(StubEvMSBuildComponent);

        public override string Eval(string data) => data;

        public StubEvMSBuildComponent()
        {

        }

        public StubEvMSBuildComponent(ISobaScript soba)
        {

        }
    }
}