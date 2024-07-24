using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookStoreApp.API.Data;
using BookStoreApp.Models.Author;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using BookStoreApp.API.Repositories;
using BookStoreApp.Models;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorsRepository authorsRepository;
        private readonly IMapper mapper;
        private readonly ILogger<AuthorsController> logger;

        public AuthorsController(IAuthorsRepository authorsRepository, IMapper mapper, ILogger<AuthorsController> logger)
        {
            this.authorsRepository = authorsRepository;
            this.mapper = mapper;
            this.logger = logger;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorReadDto>>> GetAuthors()
        {
            try
            {
                var authors = await authorsRepository.GetAllAsync();
                var authorsDtos = mapper.Map<List<AuthorReadDto>>(authors);

                return Ok(authorsDtos);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching authors");

                throw new BadHttpRequestException("An error occurred while fetching authors");
            }

        }

        // GET: api/Authors
        [HttpGet]
        [Route("page")]
        public async Task<ActionResult<VirtualizeResponse<AuthorReadDto>>> GetAuthorsPage([FromQuery] QueryParameters queryParameters)
        {
            try
            {
                //var faire = false;
                //if (faire)
                //{
                //    await authorsRepository.CreerDonneesVirtualisation();
                //}
                var reponse = await authorsRepository.GetAllAsync<Author, AuthorReadDto>(queryParameters);

                return Ok(reponse);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while fetching authors");

                throw new BadHttpRequestException("An error occurred while fetching authors");
            }

        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorReadDto>> GetAuthor(int id)
        {
            var author = await authorsRepository.GetAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            var authorDto = mapper.Map<AuthorReadDto>(author);


            return Ok(authorDto);
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> PutAuthor(int id, AuthorCreateDto authorCreateDto)
        {
            if (id != authorCreateDto.Id)
            {
                return BadRequest();
            }

            try
            {
                var author = mapper.Map<Author>(authorCreateDto);
                author.Id = id;

                await authorsRepository.UpdateAsync(author);
               }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AuthorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult<AuthorCreateDto>> PostAuthor(AuthorCreateDto authorDto)
        {
            var author = mapper.Map<Author>(authorDto);

            await authorsRepository.AddAsync(author);

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await authorsRepository.GetAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            await authorsRepository.DeleteAsync(author.Id);

            return NoContent();
        }

        private async Task<bool> AuthorExists(int id)
        {
            return (await authorsRepository.ExistAsync(id));
        }
    }
}
