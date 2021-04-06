using BM7Tutorial.DAL;
using Nexus.Base.CosmosDBRepository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BM7Tutorial.BLL.CRUD
{
    public class CRUDService
    {
        private readonly IDocumentDBRepository<Class> _repository;
        public CRUDService(IDocumentDBRepository<Class> repository)
        {
            if (_repository == null)
            {
                _repository = repository;
            }
        }

        public async Task<Class> GetClassById(string id, Dictionary<string, string> pk)
        {
            return await _repository.GetByIdAsync(id, pk);
        }
    }
}
