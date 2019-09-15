using System;
using System.Linq;
using net.r_eg.SobaScript;
using net.r_eg.Varhead;
using SobaScriptTest.Stubs;
using Xunit;

namespace SobaScriptTest
{
    public class LoaderTest
    {
        private static IUVars uvars = new UVars();

        [Fact]
        public void RegisterUnregisterTest1()
        {
            var soba = new Soba(new UVars());

            Assert.Empty(soba.Registered);
            Assert.Empty(soba.Components);

            Assert.True(soba.Register(new StubTryComponent(soba)));

            Assert.Single(soba.Registered);
            Assert.Single(soba.Components);

            Assert.True(soba.Register(new StubCommentComponent()));
            Assert.True(soba.Register(new StubBoxComponent(soba)));

            Assert.Equal(3, soba.Registered.Count());
            Assert.Equal(3, soba.Components.Count());

            Assert.True(soba.Unregister(new StubCommentComponent()));

            Assert.Equal(2, soba.Registered.Count());
            Assert.Equal(2, soba.Components.Count());

            var c1 = new StubConditionComponent(soba);
            Assert.True(soba.Register(c1));
            Assert.True(soba.Register(new StubUserVariableComponent(soba)));
            Assert.True(soba.Register(new StubEvMSBuildComponent(soba)));

            Assert.True(soba.Unregister(c1));

            Assert.Equal(4, soba.Registered.Count());
            Assert.Equal(4, soba.Components.Count());

            soba.Unregister();

            Assert.Empty(soba.Registered);
            Assert.Empty(soba.Components);
        }

        [Fact]
        public void RegisterUnregisterTest2()
        {
            var soba = new Soba(new UVars());

            soba.Register(new StubTryComponent(soba));
            soba.Register(new StubCommentComponent());
            soba.Register(new StubBoxComponent(soba));

            Assert.Throws<ArgumentNullException>(() => soba.Unregister(null));
        }

        [Fact]
        public void RegisterUnregisterTest3()
        {
            var soba = new Soba(new UVars());

            Assert.True(soba.Register(new StubTryComponent(soba)));
            Assert.True(soba.Register(new StubEvMSBuildComponent(soba)));
            Assert.True(soba.Register(new StubBoxComponent(soba)));

            Assert.False(soba.Register(new StubTryComponent(soba)));
            Assert.False(soba.Register(new StubEvMSBuildComponent(soba)));
            Assert.False(soba.Register(new StubBoxComponent(soba)));
        }

        [Fact]
        public void GetComponentTest1()
        {
            var soba = new Soba(new UVars());

            Assert.True(soba.Register(new StubTryComponent(soba)));
            Assert.True(soba.Register(new StubEvMSBuildComponent(soba)));
            Assert.True(soba.Register(new StubBoxComponent(soba)));

            Assert.Equal(typeof(StubTryComponent), soba.GetComponent<StubTryComponent>().GetType());
            Assert.Equal(typeof(StubBoxComponent), soba.GetComponent<StubBoxComponent>().GetType());

            Assert.Null(soba.GetComponent<StubUserVariableComponent>());
        }

        [Fact]
        public void GetComponentTest2()
        {
            var soba = new Soba(new UVars());

            Assert.True(soba.Register(new StubTryComponent(soba)));
            Assert.True(soba.Register(new StubEvMSBuildComponent(soba)));
            Assert.True(soba.Register(new StubBoxComponent(soba)));

            Assert.Equal(typeof(StubTryComponent), soba.GetComponent(typeof(StubTryComponent)).GetType());
            Assert.Equal(typeof(StubBoxComponent), soba.GetComponent(typeof(StubBoxComponent)).GetType());

            Assert.Null(soba.GetComponent(typeof(StubUserVariableComponent)));
        }
    }
}
