﻿using CarRental.Web.Models.Domain;

namespace CarRental.Web.Repositories;

public interface IBlogPostCommentRepository
{
    Task<BlogPostComment> AddAsync(BlogPostComment blogPostComment);
    Task<IEnumerable<BlogPostComment>> GetAllAsync(Guid blogPostId);
}