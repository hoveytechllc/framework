using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HoveyTech.Core.Model;

namespace HoveyTech.Core.EfCore.Tests
{
    public class SampleEntity : BaseEntityWithGuidKey
    {
        public string Text { get; set; }
    }

    public class SampleBaseService : BaseService
    {
        private readonly IRepository<SampleEntity> _repository;

        public SampleBaseService(IRepository<SampleEntity> repository)
        {
            _repository = repository;
        }

        protected override IHasQueryableTransaction[] ContextBasedMembers =>
            new IHasQueryableTransaction[] { _repository };
    }
}
