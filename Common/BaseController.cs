﻿using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VerticalSliceArch.Features.User;

namespace VerticalSliceArch.Common;

public class BaseController : Controller
{
    protected readonly IMediator _mediator;
    private IMapper _mapper;

    public BaseController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    protected async Task<IActionResult> Handle<T2, T3>(dynamic dto)
    {
        var queryOrCommand = _mapper.Map<T2>(dto);

        return await Handle<T3>(queryOrCommand);
    }

    protected async Task<IActionResult> Handle<T>(dynamic queryOrCommand)
    {
        if (queryOrCommand == null)
        {
            return BadRequest();
        }

        var result = new QueryOrCommandResult<T>();
        if (ModelState.IsValid)
        {
            try
            {
                result.Data = await _mediator.Send(queryOrCommand);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Messages.Add(ex.Message);
            }
        }
        else
        {
            result.Messages = ModelState.Values
                .SelectMany(m => m.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();
        }

        if (result.Success)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }
    }
}
