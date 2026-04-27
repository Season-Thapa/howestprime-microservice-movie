//using Howestprime.Movies.Application.Contracts.Ports;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Howestprime.Movies.Application.Movies;

//    /*
// MovieId: Id of the movie.
//Title: Title of the movie.
//ReleaseYear: Release year.
//Duration: Duration in minutes.
//Genres: List of genres separated by commas.
//Actors: List of actors separated by commas.
//AgeRating: Age rating of the movie.
//PosterUrl: URL to the movie poster.
//     */

//    public sealed record RegisterMovieInput(
//        Guid MovieId,
//        string Title,
//        string Description,
//        int AgeRating,
//        int ReleaseYear,
//        int Duration,
//        List<string> Genres,
//        List<string> Actors,
//        string PosterUrl
//    );
//    public sealed class RegisterMovie(
//        IUnitOfWork uow
//        ) : IUseCase<RegisterMovieInput, Guid>
//{
//    public async Task<Guid> Execute(RegisterMovieInput input)
//    {
//        MovieId movieId = new(input.MovieId);
//        var movie = Movie.Create(         
//            input.Title,
//            input.Description,
//            input.AgeRating,
//            input.ReleaseYear,
//            input.Duration,
//            input.Genres,
//            input.Actors,
//            input.PosterUrl
//        );
//        await uow.Save<IMovieRepository>(movie);
//        return movie.Id;
//    }
//}

