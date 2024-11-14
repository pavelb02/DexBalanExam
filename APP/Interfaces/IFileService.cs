using BankSystem.App.Dto;

namespace BankSystem.App.Interfaces;

public interface IFileService
{
    public Task<string> SaveFile(FileDto? file);
}