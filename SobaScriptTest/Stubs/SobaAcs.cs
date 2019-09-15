using net.r_eg.SobaScript;
using net.r_eg.Varhead;

namespace SobaScriptTest.Stubs
{
    internal static class SobaAcs
    {
        public static ISobaScript MakeNewCoreOnly() 
            => RegisterCore(new Soba());

        public static ISobaScript MakeNewCoreOnly(IUVars uvars) 
            => RegisterCore(new Soba(uvars));

        private static ISobaScript RegisterCore(ISobaScript soba)
        {
            soba.Register(new StubTryComponent(soba));
            soba.Register(new StubCommentComponent());
            soba.Register(new StubBoxComponent(soba));
            soba.Register(new StubConditionComponent(soba));
            soba.Register(new StubUserVariableComponent(soba));
            soba.Register(new StubEvMSBuildComponent(soba));

            return soba;
        }
    }
}