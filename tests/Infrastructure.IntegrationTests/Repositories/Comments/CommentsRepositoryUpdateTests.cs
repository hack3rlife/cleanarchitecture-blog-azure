﻿using System;
using System.Threading.Tasks;
using BlogWebApi.Application.Interfaces;
using BlogWebApi.Domain;
using BlogWebApi.Infrastructure.Repositories;
using Infrastructure.IntegrationTests.Builders;
using LoremNET;
using Xunit;

namespace Infrastructure.IntegrationTests.Repositories.Comments
{
    [Collection("DatabaseCollectionFixture")]
   public class CommentsRepositoryUpdateTests
    {
        private readonly DatabaseFixture _fixture;
        private readonly IBlogRepository _blogRepository;
        private readonly IPostRepository _postRepository;
        private readonly ICommentRepository _commentRepository;

        public CommentsRepositoryUpdateTests(DatabaseFixture fixture)
        {
            _fixture = fixture;
            _blogRepository = new BlogRepository(fixture.BlogDbContext);
            _postRepository = new PostRepository(fixture.BlogDbContext);
            _commentRepository = new CommentRepository(fixture.BlogDbContext);
        }

        [Fact(DisplayName = "CommentRepository_UpdateComment_Success")]
        public async Task CommentRepository_UpdateComment_Success()
        {
            //Arrange
            var newBlog = BlogBuilder.Create().Build();
            var blog = await _blogRepository.AddAsync(newBlog);

            var newPost = PostBuilder.Create()
                .WithPostId(Guid.NewGuid())
                .WithName(Lorem.Words(10))
                .WithText(Lorem.Sentence(100))
                .WithBlogId(blog.BlogId)
                .Build();

            var post = await _postRepository.AddAsync(newPost);

            var newComment = new Comment
            {
                CommentId = Guid.NewGuid(),
                CommentName = Lorem.Sentence(5),
                Email = Lorem.Email(),
                PostId = post.PostId,
            };
            var comment = await _commentRepository.AddAsync(newComment);
            comment.CommentName = Lorem.Sentence(5);

            //Act
            await _commentRepository.UpdateAsync(comment);

            //Assert
            var updatedComment = await _commentRepository.GetByIdAsync(comment.CommentId);

            Assert.NotNull(updatedComment);
            Assert.Equal(comment.CommentId, updatedComment.CommentId);
            Assert.Equal(comment.CommentName, updatedComment.CommentName);
            Assert.Equal(comment.Email, updatedComment.Email);
            Assert.Equal(comment.PostId, updatedComment.PostId);
        }
    }
}
