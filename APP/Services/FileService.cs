using BankSystem.App.Dto;
using BankSystem.App.Interfaces;

namespace APP.Services;

public class FileService : IFileService
{
    string pathToDirectory = Path.Combine("D:", "Программирование","Dex backend 2024", "ExamDexBalan", "Images");

    public async Task<string> SaveFile(FileDto? file)
    {

        var filePath = Path.Combine(pathToDirectory, file.FileName);

        try
        {
            if (!Directory.Exists(pathToDirectory))
            {
                Directory.CreateDirectory(pathToDirectory);
            }

            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.Stream.CopyToAsync(fileStream);
            }
        }
        catch (DirectoryNotFoundException ex)
        {
            throw new DirectoryNotFoundException($"Директория не найдена: {pathToDirectory}. {ex.Message}", ex);
        }
        catch (IOException ex)
        {
            throw new IOException($"Ошибка при сохранении файла: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}\nТрассировка стека: {ex.StackTrace}");
            throw;
        }

        return Path.Combine(pathToDirectory, file.FileName);
    }
}