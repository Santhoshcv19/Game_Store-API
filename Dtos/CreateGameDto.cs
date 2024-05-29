using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Dtos;

/// <summary>
/// Represents a game entity.
/// </summary>
/// <param name="Name">The name of the game. Maximum length is 50 characters.</param>
/// <param name="GenreId">The ID of the genre. This should be a valid ID from the genres table.</param>
/// <param name="Price">The price of the game. Must be between 1 and 100.</param>
/// <param name="ReleaseDate">The release date of the game.</param>
public record class CreateGameDto(
    [Required][StringLength(50)] string Name, 
    int GenreId, 
    [Range(1, 100)] decimal Price, 
    DateOnly ReleaseDate
);
