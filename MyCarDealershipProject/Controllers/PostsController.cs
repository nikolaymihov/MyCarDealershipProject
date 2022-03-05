﻿namespace MyCarDealershipProject.Controllers
{
    using System;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Authorization;
    using AutoMapper;
    using Models.Cars;
    using Models.Posts;
    using Models.Images;
    using Infrastructure;
    using Services.Cars;
    using Services.Posts;
    using Services.Cars.Models;
    using Services.Images.Models;
    using Services.Posts.Models;

    public class PostsController : Controller
    {
        private const int PostsPerPage = 12;

        private readonly ICarsService carsService;
        private readonly IPostsService postsService;
        private readonly IWebHostEnvironment environment;
        private readonly IMapper mapper;

        public PostsController(ICarsService carsService, IPostsService postsService, IWebHostEnvironment environment, IMapper mapper)
        {
            this.carsService = carsService;
            this.postsService = postsService;
            this.environment = environment;
            this.mapper = mapper;
        }

        [Authorize]
        public IActionResult Create()
        {
            var createPostServiceModel = new PostFormInputModelDTO();
            var createCarServiceModel = new CarFormInputModelDTO();

            this.carsService.FillInputCarBaseProperties(createCarServiceModel);

            createPostServiceModel.Car = createCarServiceModel;

            var createPostViewModel = this.mapper.Map<PostFormInputModel>(createPostServiceModel);
            var createCarViewModel = this.mapper.Map<CarFormInputModel>(createCarServiceModel);

            createPostViewModel.Car = createCarViewModel;

            return this.View(createPostViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(PostFormInputModel input)
        {
            var inputCarDTO = this.mapper.Map<CarFormInputModelDTO>(input.Car);

            if (!this.ModelState.IsValid)
            {
                this.carsService.FillInputCarBaseProperties(inputCarDTO);
                input.Car = this.mapper.Map<CarFormInputModel>(inputCarDTO);
                return this.View(input);
            }

            var userId = this.GetCurrentUserId();
            var inputPostDTO = this.mapper.Map<PostFormInputModelDTO>(input);
            var selectedExtrasIds = input.SelectedExtrasIds.ToList();
            var imagePath = $"{this.environment.WebRootPath}/images";
            int postId;

            try
            {
                var car = await this.carsService.GetCarFromInputModelAsync(inputCarDTO, selectedExtrasIds, userId, imagePath);
                postId = await this.postsService.CreateAsync(inputPostDTO, car, userId);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("CustomError", ex.Message);
                this.carsService.FillInputCarBaseProperties(inputCarDTO);
                input.Car = this.mapper.Map<CarFormInputModel>(inputCarDTO);
                return this.View(input);
            }

            TempData[GlobalConstants.GlobalSuccessMessageKey] = "Your car post was added successfully!";

            return this.RedirectToAction("Offer", new { Id = postId });
        }

        public IActionResult Search()
        {
            var searchCarDTO = new SearchCarInputModelDTO();
            var searchPostDTO = new SearchPostDTO();

            this.carsService.FillInputCarBaseProperties(searchCarDTO);

            searchPostDTO.Car = searchCarDTO;

            var searchCarInputModel = this.mapper.Map<SearchCarInputModel>(searchCarDTO);
            var searchPostInputModel = this.mapper.Map<SearchPostInputModel>(searchPostDTO);
            
            searchPostInputModel.Car = searchCarInputModel;

            return this.View(searchPostInputModel);
        }

        public IActionResult All(SearchPostInputModel searchPostInputModel, int id = 1, int sorting = 0)
        {
            try
            {
                if (id <= 0)
                {
                    return this.NotFound();
                }

                var searchPostDTO = this.mapper.Map<SearchPostDTO>(searchPostInputModel);
                var matchingPosts = this.postsService.GetMatchingPosts(searchPostDTO, sorting).ToList();
                var postsByPageDTOs = this.postsService.GetPostsByPage(matchingPosts, id, PostsPerPage);
                var postsByPageAsViewModels = this.mapper.Map<IEnumerable<PostInListDTO>, IEnumerable<PostInListViewModel>>(postsByPageDTOs);

                var postsListViewModel = new PostsListViewModel()
                {
                    PageNumber = id,
                    PostsPerPage = PostsPerPage,
                    PostsCount = matchingPosts.Count,
                    Posts = postsByPageAsViewModels,
                };

                if (id > postsListViewModel.PagesCount)
                {
                    return this.NotFound();
                }

                return this.View(postsListViewModel);
            }
            catch (Exception ex)
            {
                this.TempData[GlobalConstants.GlobalErrorMessageKey] = ex.Message;
                
                var searchCarInputModel = searchPostInputModel.Car;
                var searchCarInputModelDTO = this.mapper.Map<SearchCarInputModelDTO>(searchCarInputModel);
                
                this.carsService.FillInputCarBaseProperties(searchCarInputModelDTO);

                searchCarInputModel = this.mapper.Map<SearchCarInputModel>(searchCarInputModelDTO);
                searchPostInputModel.Car = searchCarInputModel;

                return this.View("Search", searchPostInputModel);
            }
        }

        public IActionResult Offer(int id)
        {
            var singlePostDataDTO = this.postsService.GetSinglePostViewModelById(id);

            if (singlePostDataDTO == null)
            {
                return this.NotFound();
            }
            
            var singlePostViewModel = this.mapper.Map<SinglePostViewModel>(singlePostDataDTO);

            return this.View(singlePostViewModel);
        }

        [Authorize]
        public IActionResult Mine(int id = 1, int sorting = 0)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }
            
            var userId = this.GetCurrentUserId();
            var postsByUserDTOs = this.postsService.GetPostsByUser(userId, sorting).ToList();
            var postsByUserDTOsForThisPage = this.postsService.GetPostsByPage(postsByUserDTOs, id, PostsPerPage);
            var postsByUserViewModels = this.mapper.Map<IEnumerable<PostByUserDTO>, IEnumerable<PostByUserViewModel>>(postsByUserDTOsForThisPage);

            var postsByUserViewModel = new PostsByUserViewModel()
            {
                PageNumber = id,
                PostsPerPage = PostsPerPage,
                PostsCount = postsByUserDTOs.Count,
                Posts = postsByUserViewModels,
            };

            if (id > postsByUserViewModel.PagesCount && id > 1)
            {
                return this.NotFound();
            }

            return this.View(postsByUserViewModel);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var userId = this.GetCurrentUserId();
            var editPostDTO = this.postsService.GetPostFormInputModelById(id);

            if (editPostDTO == null)
            {
                return NotFound();
            }

            if (editPostDTO.CreatorId != userId)
            {
                return Unauthorized();
            }

            this.carsService.FillInputCarBaseProperties(editPostDTO.Car);

            var editPostViewModel = this.mapper.Map<EditPostViewModel>(editPostDTO);

            return this.View(editPostViewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditPostViewModel editedPost)
        {
            var userId = this.GetCurrentUserId();

            if (editedPost.CreatorId != userId)
            {
                return Unauthorized();
            }
            
            var editedPostDTO = this.mapper.Map<EditPostDTO>(editedPost);
            var currentImagesDTO = this.postsService.GetCurrentDbImagesForAPost(id);
            var currentImagesViewModel = this.mapper.Map<IEnumerable<ImageInfoDTO>, IEnumerable<ImageInfoViewModel>>(currentImagesDTO);

            if (!this.ModelState.IsValid)
            {
                this.carsService.FillInputCarBaseProperties(editedPostDTO.Car);

                var editedPostViewModel = this.mapper.Map<EditPostViewModel>(editedPostDTO);
                editedPostViewModel.CurrentImages = currentImagesViewModel;

                return this.View(editedPostViewModel);
            }

            var selectedExtrasIds = editedPost.SelectedExtrasIds.ToList();
            var deletedImagesIds = editedPost.DeletedImagesIds.ToList();
            var imagePath = $"{this.environment.WebRootPath}/images";

            try
            {
                await this.carsService.UpdateCarDataFromInputModelAsync(editedPostDTO.CarId, editedPostDTO.Car, selectedExtrasIds, deletedImagesIds, userId, imagePath, editedPost.SelectedCoverImageId);
                await this.postsService.UpdateAsync(id, editedPostDTO);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError("CustomError", ex.Message);
                this.carsService.FillInputCarBaseProperties(editedPostDTO.Car);

                editedPost = this.mapper.Map<EditPostViewModel>(editedPostDTO);
                editedPost.CurrentImages = currentImagesViewModel;

                return this.View(editedPost);
            }

            TempData[GlobalConstants.GlobalSuccessMessageKey] = "Your car post was edited successfully!";

            return this.RedirectToAction("Offer", new { Id = id });
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            var userId = this.GetCurrentUserId();
            var postDTO = this.postsService.GetBasicPostInformationById(id);

            if (postDTO == null)
            {
                return NotFound();
            }

            var postCreatorId = this.postsService.GetPostCreatorId(id);

            if (userId != postCreatorId)
            {
                return Unauthorized();
            }

            var postByUserViewModel = this.mapper.Map<PostByUserViewModel>(postDTO);

            return this.View(postByUserViewModel);
        }

        [HttpPost]
        [Authorize]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = this.GetCurrentUserId();
            var postCreatorId = this.postsService.GetPostCreatorId(id);

            if (userId != postCreatorId)
            {
                return Unauthorized();
            }

            try
            {
                await this.postsService.DeletePostByIdAsync(id);
                TempData[GlobalConstants.GlobalSuccessMessageKey] = "Your car post was deleted successfully!";
            }
            catch (Exception ex)
            {
                this.TempData[GlobalConstants.GlobalErrorMessageKey] = ex.Message;
            }

            return this.RedirectToAction("Mine");
        }

        private string GetCurrentUserId()
        {
            return this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}