using EnterpriseInventory.BAL.Interfaces;
using EnterpriseInventory.BAL.ModelsDTO;
using EnterpriseInventory.WEB.Common;
using EnterpriseInventory.WEB.ViewModels;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseInventory.WEB.Controllers
{
    [ApiController]
    [Route("api/cabinet/")]
    [EnableCors]
    public class CabinetControlle : ControllerBase
    {
        ICabinetService cabinetService;
        public CabinetControlle(ICabinetService _cabinetService)
        {
            cabinetService = _cabinetService;
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddCabinet([FromBody] CabinetVM cabinet)
        {
            if (cabinet == null)
                return BadRequest(new Response<CabinetVM> { StatusCode = 500, Message = "Уебок, кабинет пустой!" });

            CabinetDTO cab = new()
            {
                Name = cabinet.Name,
                Owner = cabinet.Owner,
            };

            cabinetService.AddCabinet(cab);
            return Ok(new Response<CabinetVM> { StatusCode = 200, Data = cabinet });

        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            return Ok(new Response<IEnumerable<CabinetDTO>> { Data = cabinetService.GetAllCabinets(), StatusCode = 200, Message = "" });
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var _cab = cabinetService.GetCabinetById(id);
            if (_cab != null)
                return Ok(new Response<CabinetDTO> { StatusCode = 200, Data = _cab });

            return NotFound(new Response<CabinetDTO> { StatusCode = 404, Message = "Такого кабинета не существует." });
        }

        [HttpGet]
        [Route("{owner}")]
        public IActionResult GetByOwner(string owner)
        {
            var _cab = cabinetService.GetCabinetByOwner(owner);
            if (_cab != null)
                return Ok(new Response<List<CabinetDTO>> { StatusCode = 200, Data = _cab.ToList() });
            return NotFound(new Response<CabinetDTO> { StatusCode = 404, Message = "За этим человенком не закрепленно ни одного кабинета." });
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update(CabinetDTO cabinet)
        {
            if (cabinet == null) return BadRequest(new Response<CabinetDTO> { StatusCode = 400, Message = "Входящие данные не могут быть пустыми." });

            cabinetService.UpdateCabinet(cabinet);

            return Ok(new Response<CabinetDTO> { StatusCode = 200, Data = cabinet });
        }

        [HttpDelete]
        [Route("remove")]
        public IActionResult Remove(int id)
        {
            if (id < 0) return BadRequest(new Response<CabinetDTO> { StatusCode = 400, Message = "ID не может быть меньше нуля!"});

            var _cab = cabinetService.GetCabinetById(id);

            if (_cab == null) return NotFound(new Response<CabinetDTO> { StatusCode = 404, Message = $"Кабинет с Id = {id} не найден." });

            cabinetService.DeleteCabinet(id);
            return Ok(new Response<CabinetDTO> { StatusCode = 200, Message = "Кабинет успешно удален." });
        }
    }
}
