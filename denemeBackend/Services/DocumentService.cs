using denemeBackend.DTOs;
using denemeBackend.Models;
using denemeBackend.Repositories;

namespace denemeBackend.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IDocumentRepository _documentRepository;

        public DocumentService(IDocumentRepository documentRepository)
        {
            _documentRepository = documentRepository;
        }

        public async Task<DocumentDto?> GetByIdAsync(Guid id)
        {
            var document = await _documentRepository.GetByIdAsync(id);
            return document == null ? null : new DocumentDto(
                document.Id,
                document.ClientId,
                document.FileName,
                document.FileUrl,
                document.UploadedAt
            );
        }

        public async Task<IEnumerable<DocumentDto>> GetAllAsync()
        {
            var documents = await _documentRepository.GetAllAsync();
            return documents.Select(d => new DocumentDto(
                d.Id,
                d.ClientId,
                d.FileName,
                d.FileUrl,
                d.UploadedAt
            ));
        }

        public async Task<IEnumerable<DocumentDto>> GetByClientIdAsync(Guid clientId)
        {
            var documents = await _documentRepository.GetByClientIdAsync(clientId);
            return documents.Select(d => new DocumentDto(
                d.Id,
                d.ClientId,
                d.FileName,
                d.FileUrl,
                d.UploadedAt
            ));
        }

        public async Task<DocumentDto> CreateAsync(CreateDocumentDto dto)
        {
            var document = new Document
            {
                Id = Guid.NewGuid(),
                ClientId = dto.ClientId,
                FileName = dto.FileName,
                FileUrl = dto.FileUrl,
                UploadedAt = DateTime.UtcNow
            };

            await _documentRepository.AddAsync(document);
            await _documentRepository.SaveChangesAsync();

            return new DocumentDto(
                document.Id,
                document.ClientId,
                document.FileName,
                document.FileUrl,
                document.UploadedAt
            );
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateDocumentDto dto)
        {
            var document = await _documentRepository.GetByIdAsync(id);
            if (document == null) return false;

            document.FileName = dto.FileName;
            document.FileUrl = dto.FileUrl;

            _documentRepository.Update(document);
            await _documentRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var document = await _documentRepository.GetByIdAsync(id);
            if (document == null) return false;

            _documentRepository.Delete(document);
            await _documentRepository.SaveChangesAsync();
            return true;
        }
    }
} 