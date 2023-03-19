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
    [Route("api/item/")]
    [EnableCors]
    public class ItemController : ControllerBase
    {
        IItemService itemService;
        public ItemController(IItemService _itemService)
        {
            itemService = _itemService;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            var cabinets = itemService.GetAll();
            return Ok(new Response<List<ItemDTO>> { StatusCode = 200, Data = cabinets });
        }

        [HttpPost]
        [Route("add")]
        public IActionResult AddItem(ItemVM item)
        {
            if (item == null) return BadRequest(new Response<ItemVM> { StatusCode = 400, Message = "Предмет не может быть пустым!" });

            ItemDTO _item = new()
            {
                Name = item.Name,
                Article = item.Article,
                CabinetName = item.CabinetName
            };

            itemService.AddItemAsync(_item);

            return Ok(new Response<ItemVM> { StatusCode = 200, Data = item });
        }

        [HttpPost]
        [Route("update")]
        public IActionResult Update(ItemDTO item)
        {
            if (item == null) return BadRequest(new Response<ItemDTO> { StatusCode = 400, Message = "Предмет не может быть пустым!" });

            itemService.UpdateItem(item);
            return Ok(new Response<ItemDTO> { StatusCode = 200, Message = "Данные успешно изменены.", Data = item });
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            if (id < 0) return BadRequest(new Response<ItemDTO> { StatusCode = 400, Message = "ID не может быть меньше нуля!" });

            var _item = itemService.GetItemById(id);

            if (_item == null) return NotFound(new Response<ItemDTO> { StatusCode = 404, Message = $"Предмет с Id = {id} не найден." });

            return Ok(new Response<ItemDTO> { StatusCode = 200, Data = _item });
        }

        [HttpDelete]
        [Route("remove")]
        public IActionResult Delete(int id)
        {
            if (id < 0) return BadRequest(new Response<ItemDTO> { StatusCode = 400, Message = "ID не может быть меньше нуля!" });
            var _item = itemService.GetItemById(id);
            if (_item == null) return NotFound(new Response<ItemDTO> { StatusCode = 404, Message = $"Предмет с Id = {id} не найден." });
            itemService.RemoveItemAsync(id);
            return Ok(new Response<ItemDTO> { StatusCode = 200, Message = "Предмет успешно удален."});
        }

        [HttpGet]
        [Route("getByName/{name}")]
        public IActionResult GetByName(string name)
        {
            if (name == string.Empty) return BadRequest(new Response<ItemDTO> { StatusCode = 400, Message = "Строка имени не может быть пустой." });
            var result = itemService.GetItemByName(name);

            return Ok(new Response<List<ItemDTO>> { StatusCode = 200, Data = result});
        }
    }
}
