using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Controllers.Resources;
using Vega.Core.Models;
using Vega.Persistance;

namespace Vega.Controllers
{
    [Route("api/models")]
    [ApiController]
    public class ModelController : ControllerBase
    {

        private readonly VegaDbContext context;
        private readonly IMapper mapper;

        public ModelController(VegaDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<KeyValuePairResource>> GetModel()
        {
            var models = await context.Models.ToListAsync();

            return mapper.Map<List<Model>, List<KeyValuePairResource>>(models);
        }

    }
}