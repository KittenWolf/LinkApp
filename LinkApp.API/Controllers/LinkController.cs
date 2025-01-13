using LinkApp.API.Requests;
using LinkApp.API.Responses;
using LinkApp.API.Services;
using LinkApp.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace LinkApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LinkController(ILinksRepository linkRepository) : ControllerBase
    {
        private readonly LinkShorterService _linkShorterService = new();
        private readonly ILinksRepository _linkRepository = linkRepository;

        [HttpPost]
        public async Task<ActionResult> CreateLink([FromBody] LinkCreateRequest link)
        {
            var result = _linkShorterService.Short(link.LongLink);

            if (await IsTokenExists(result))
            {
                return NoContent();
            }

            await _linkRepository.Create(result, link.LongLink);

            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<List<LinksResponse>>> GetAllLinks()
        {
            var links = await _linkRepository.GetAll();
            var result = links.Select(l => new LinksResponse(l.Token, l.LongLink));

            return Ok(result);
        }

        [HttpGet("{token}")]
        public async Task<ActionResult> GetLinkByToken(string token)
        {
            if (!await IsTokenExists(token))
            {
                return NotFound();
            }

            // Возможно стоит делать редирект.
            return Ok();            
        }

        [HttpPut("{token}")]
        public async Task<ActionResult> UpdateLink(string token, [FromBody] LinkUpdateRequest link)
        {
            if (!await IsTokenExists(token))
            {
                return NotFound();
            }

            var result = await _linkRepository.Update(token, link.LongLink);

            return Ok($"Link with token: {result} is updated");
        }


        [HttpDelete("{token}")]
        public async Task<ActionResult> DeleteLink(string token)
        {
            if (!await IsTokenExists(token))
            {
                return NotFound();
            }

            var result = await _linkRepository.Delete(token);

            return Ok($"Link with token: {result} is deleted");
        }

        private async Task<bool> IsTokenExists(string token)
        {
            var result = await _linkRepository.GetByToken(token);

            if (string.IsNullOrEmpty(result))
            {
                return false;
            }

            return true;
        }
    }
}
