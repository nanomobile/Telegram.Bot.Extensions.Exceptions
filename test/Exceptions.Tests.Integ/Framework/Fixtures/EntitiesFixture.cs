using System.Collections.Generic;

namespace Exceptions.Tests.Integ.Framework.Fixtures
{
    public class EntitiesFixture<TEntity>
    {
        public List<TEntity> Entities { get; set; }
    }
}
