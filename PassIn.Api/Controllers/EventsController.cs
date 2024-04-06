<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PassIn.Application.UseCases.Events.GetById;
=======
﻿using Microsoft.AspNetCore.Mvc;
>>>>>>> 796eb8c06301e4da3eaca30f25d423904e3c03b8
using PassIn.Application.UseCases.Events.Register;
using PassIn.Communication.Requests;
using PassIn.Communication.Responses;
using PassIn.Exceptions;

namespace PassIn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredEventJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register([FromBody] RequestEventJson request)
        {
            try
            {
                var useCase = new RegisterEventUseCase();

<<<<<<< HEAD
                var response = useCase.Execute(request);

                return Created(string.Empty, response);
=======
                useCase.Execute(request);

                return Created();
>>>>>>> 796eb8c06301e4da3eaca30f25d423904e3c03b8
            }
            catch (PassInException ex) 
            {
                return BadRequest(new ResponseErrorJson(ex.Message));
            }
            catch
            {
<<<<<<< HEAD
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorJson("Unknow error"));
            }
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(ResponseEventJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorJson), StatusCodes.Status404NotFound)]
        public IActionResult GetById([FromRoute] Guid id)
        {
            try
            {
                var useCase = new GetEventByIdUseCase();

                var response = useCase.Execute(id);

                return Ok(response);
            }
            catch (PassInException ex)
            {
                return NotFound(new ResponseErrorJson(ex.Message));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorJson("Unknow error"));
=======
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorJson("Errp desconhecido"));
>>>>>>> 796eb8c06301e4da3eaca30f25d423904e3c03b8
            }
        }
    }
}
