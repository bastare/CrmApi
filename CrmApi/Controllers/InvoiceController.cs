using AutoMapper;
using CrmWebApi.Data;
using CrmWebApi.DTOs;
using KostenVoranSchlagConsoleParser.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace CrmWebApi.Controllers
{
    [Route("api/Invoice")]
    public class InvoiceController : ControllerBase
    {
        readonly IMapper _mapper;

        readonly InvoiceRepository _repo = new InvoiceRepository(ConnectHelper.CrmService);

        public InvoiceController(IMapper mapper)//, InvoiceRepository repo)
        {
            _mapper = mapper
                ?? throw new ArgumentNullException(typeof(IMapper).FullName, "Ninject doesn`t bind, current type");

            //_repo = repo
            //    ?? throw new ArgumentNullException(typeof(InvoiceRepository).FullName, "Ninject doesn`t bind, current type");
        }

        [HttpGet]
        [Route(nameof(GetInvoiceData))]
        public async Task<IActionResult> GetInvoiceData()
        {
            IEnumerable<Entity> data = await _repo.GetInvoiceDataByStates(2);

            var result = _mapper.Map<IEnumerable<Entity>, IEnumerable<InvoiceDataForViewDTO>>(data);

            return Ok(result);
        }
    }
}