using ElleChristine.API.Dtos;

namespace ElleChristine.API.Web.Controllers.ResponseHelpers
{
    #pragma warning disable CS1591

    public static class UriLinkHelper
    {
        public static ShowDto CreateLinksForShow(HttpRequest request, ShowDto show)
        {
            string protocol = (request.IsHttps) ? "https" : "http";
            try
            {
                show.Links.Add(new LinkDto($"{protocol}://{request.Host}/api/shows/{show.Id}", "self", "GET"));
                show.Links.Add(new LinkDto($"{protocol}://{request.Host}/api/shows", "show-collection", "GET"));
            }
            catch
            {
                throw;
            }
            return show;
        }

        public static LinkDto CreateLinkForShowWithinCollection(HttpRequest request, ShowDto show)
        {
            string protocol = (request.IsHttps) ? "https" : "http";
            try
            {
                LinkDto link = new LinkDto($"{protocol}://{request.Host}/api/shows/{show.Id}", "show", "GET");
                return link;
            }
            catch
            {
                throw;
            }
        }

        public static PhotoDto CreateLinksForPhoto(HttpRequest request, PhotoDto photo)
        {
            string protocol = (request.IsHttps) ? "https" : "http";
            try
            {
                photo.Links.Add(new LinkDto($"{protocol}://{request.Host}/api/shows/{photo.Id}", "self", "GET"));
                photo.Links.Add(new LinkDto($"{protocol}://{request.Host}/api/shows", "show-collection", "GET"));
            }
            catch
            {
                throw;
            }
            return photo;
        }

        public static LinkDto CreateLinkForPhotoWithinCollection(HttpRequest request, PhotoDto photo)
        {
            string protocol = (request.IsHttps) ? "https" : "http";
            try
            {
                LinkDto link = new LinkDto($"{protocol}://{request.Host}/api/shows/{photo.Id}", "show", "GET");
                return link;
            }
            catch
            {
                throw;
            }
        }
    }

    #pragma warning restore CS1591
}
