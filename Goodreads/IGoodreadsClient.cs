﻿using Goodreads.Clients;

namespace Goodreads
{
    /// <summary>
    /// The API interface for accessing the Goodreads API.
    /// </summary>
    public interface IGoodreadsClient
    {
        /// <summary>
        /// API Client for the Goodreads Authors endpoint.
        /// </summary>
        IAuthorsClient Authors { get; }

        /// <summary>
        /// API Client for the Goodreads Books endpoint.
        /// </summary>
        IBooksClient Books { get; }

        /// <summary>
        /// API Client for the Goodreads Shelves endpoint.
        /// </summary>
        IShelvesClient Shelves { get; }

        /// <summary>
        /// API Client for the Goodreads Users endpoint.
        /// </summary>
        IUsersClient Users { get; }

        /// <summary>
        /// API Client for the Goodreads Reviews endpoint.
        /// </summary>
        IReviewsClient Reviews { get; }

        /// <summary>
        /// API Client for the Goodreads Series endpoint.
        /// </summary>
        ISeriesClient Series { get; }

        /// <summary>
        /// API Client for the Goodreads Author_following endpoint.
        /// </summary>
        IAuthorsFollowingClient AuthorsFollowing { get; }

        /// <summary>
        /// API Client for the Goodreads Events endpoint.
        /// </summary>
        IEventsClient Events { get; }

        /// <summary>
        /// API Client for the Goodreads user followers endpoint.
        /// </summary>
        IFollowersClient Followers { get; }

        /// <summary>
        /// API Client for the Goodreads user friends endpoint.
        /// </summary>
        IFriendsClient Friends { get; }

        /// <summary>
        /// API Client for the Goodreads notifications endpoint.
        /// </summary>
        INotificationsClient Notifications { get; }
    }
}
