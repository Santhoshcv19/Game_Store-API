using System.ComponentModel.DataAnnotations;

namespace GameStore.Api.Entities;

/// <summary>
/// Represents a game entity.
/// </summary>
public class Game
{
    public int Id { get; set; }

    /// <summary>
    /// The name of the game.
    /// </summary>
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }

    public int GenreId { get; set; }

    public Genre? Genre { get; set; }

    /// <summary>
    /// The price of the game. Must be between $1 and $100.
    /// </summary>
    [Range(1, 100, ErrorMessage = "Price must be between $1 and $100")]
    public decimal Price { get; set; }

    public DateOnly ReleaseDate { get; set; }
}
