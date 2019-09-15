using net.r_eg.SobaScript;
using net.r_eg.SobaScript.Components;

namespace SobaScriptTest.Stubs
{
    internal sealed class StubCommentComponent: ComponentAbstract
    {
        public override string Activator => nameof(StubTryComponent);

        public override string Eval(string data) => data;

        public StubCommentComponent()
        {

        }

        public StubCommentComponent(ISobaScript soba)
        {

        }
    }
}