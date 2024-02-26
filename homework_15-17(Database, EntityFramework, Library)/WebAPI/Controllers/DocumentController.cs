using Common.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentRepository _documentRepository;
        private readonly ILogger<DocumentController> _logger;

        public DocumentController(IDocumentRepository documentRepository, ILogger<DocumentController> logger)
        {
            this._documentRepository = documentRepository;
            this._logger = logger;
        }

        [AllowAnonymous]
        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            _logger.LogInformation($"Get list of documents");

            var documents = await _documentRepository.GetDocuments();
            if (documents != null)
                return Ok(documents);

            return NotFound("Documents not found!");
        }
    }
}
