﻿using System.Net;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Bloggie.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ImagesController : Controller
{
    private readonly IImageRepository _imageRepository;

    public ImagesController(IImageRepository imageRepository)
    {
        _imageRepository = imageRepository;
    }

    [HttpPost]
    public async Task<IActionResult> UploadAsync(IFormFile file)
    {
        var imageUrl = await _imageRepository.UploadAsync(file);

        if (imageUrl == null)
            return Problem("Something went wrong", null, (int)HttpStatusCode.InternalServerError);
        return Json(new { link = imageUrl });
    }
}