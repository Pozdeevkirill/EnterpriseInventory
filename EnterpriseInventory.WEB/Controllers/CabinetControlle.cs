using EnterpriseInventory.BAL.Interfaces;
using EnterpriseInventory.BAL.ModelsDTO;
using EnterpriseInventory.WEB.Common;
using EnterpriseInventory.WEB.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseInventory.WEB.Controllers
{
    [ApiController]
    [Route("api/")]
    public class CabinetControlle : ControllerBase
    {
        ICabinetService cabinetService;
        public CabinetControlle(ICabinetService _cabinetService)
        {
            cabinetService = _cabinetService;
        }

        [HttpPost]
        [Route("AddCabinet")]
        public IActionResult AddCabinet([FromBody] CabinetVM cabinet)
        {
            if(cabinet == null)
                return BadRequest(new Response<CabinetVM> { StatusCode = 500, Message = "Уебок, кабинет пустой!"});

            CabinetDTO cab = new()
            {
                Name = cabinet.Name,
                Owner = cabinet.Owner,
            };

            cabinetService.AddCabinet(cab);
            return Ok(new Response<CabinetVM> {StatusCode = 200, Data = cabinet });

        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(new Response<IEnumerable<CabinetDTO>> { Data = cabinetService.GetAllCabinets(), StatusCode=200,Message = ""});
        }
    }
}
